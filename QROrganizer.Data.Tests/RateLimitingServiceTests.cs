using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using QROrganizer.Data.Services.Implementation;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Tests.Util;
using Xunit;

namespace QROrganizer.Data.Tests;

public class RateLimitingServiceTests
{
    private HttpRequestMessage MockHttpRequestMessage => new(HttpMethod.Get, "https://testUrl.com/");

    [Fact]
    public async Task RateLimitingService_ExceptionThrown_StillCompletes()
    {
        using var rlq = new RateLimitingService(TimeSpan.Zero);

        var mockHandler = new MockHttpMessageHandler(() => Task.CompletedTask);
        var httpClient = new HttpClient(mockHandler);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            rlq.EnqueueHttpRequest(httpClient, new HttpRequestMessage()));
    }

    [Fact]
    public async Task RateLimitingService_ExceptionThrown_StillProcessesRemainingItems()
    {
        using var rlq = new RateLimitingService(TimeSpan.Zero);

        var mockHandler = new MockHttpMessageHandler(() => Task.CompletedTask, () => Task.CompletedTask);
        var httpClient = new HttpClient(mockHandler);

        var task2 = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            rlq.EnqueueHttpRequest(httpClient, new HttpRequestMessage()));

        var res = await task2;
        Assert.NotNull(res);
    }

    [Fact]
    public async Task RateLimitingService_ThingGetsProcessed()
    {
        using var rlq = new RateLimitingService(TimeSpan.Zero);

        bool wasRun = false;

        Task<bool> MyAction()
        {
            wasRun = true;
            return Task.FromResult(true);
        }

        var mockHandler = new MockHttpMessageHandler(MyAction);
        var httpClient = new HttpClient(mockHandler);

        var res = await rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);

        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.True(wasRun);
    }

    [Fact]
    public async Task RateLimitingService_TasksDelayed_DelayBetweenTwoRequests()
    {
        var mockDelay = new Mock<IAsyncDelay>();
        mockDelay.Setup(x => x.Delay(It.IsAny<TimeSpan>()))
            .Returns(Task.CompletedTask);
        using var rlq = new RateLimitingService(TimeSpan.FromMilliseconds(100), mockDelay.Object);

        Task<bool> MyAction1()
        {
            return Task.FromResult(true);
        }

        Task<bool> MyAction2()
        {
            return Task.FromResult(true);
        }

        var mockHandler = new MockHttpMessageHandler(MyAction1, MyAction2);
        var httpClient = new HttpClient(mockHandler);

        var task1 = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);
        var task2 = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);

        await task1;
        await task2;
        
        mockDelay.Verify(delay => delay.Delay(It.IsAny<TimeSpan>()), Times.Exactly(2));
    }


    [Fact]
    public async Task RateLimitingService_TasksCompletedInOrderQueued()
    {
        using var rlq = new RateLimitingService(TimeSpan.Zero);
        var mre = new ManualResetEventSlim();
        var mre2 = new ManualResetEventSlim();

        bool wasRun = false;

        Task<bool> FirstAction()
        {
            mre.Set();
            mre2.Wait();
            wasRun = true;
            return Task.FromResult(true);
        }
        Task<bool> SecondAction() => Task.FromResult(true);

        var mockHandler = new MockHttpMessageHandler(FirstAction, SecondAction);
        var httpClient = new HttpClient(mockHandler);

        var firstTask = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);
        var secondTask = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);

        mre.Wait();

        Assert.Equal(TaskStatus.WaitingForActivation, secondTask.Status);

        mre2.Set();
        await firstTask;

        Assert.Equal(TaskStatus.RanToCompletion, firstTask.Status);

        await secondTask;
        Assert.Equal(TaskStatus.RanToCompletion, firstTask.Status);
        Assert.Equal(TaskStatus.RanToCompletion, secondTask.Status);
        Assert.True(wasRun);
    }

    [Fact]
    public async Task RateLimitingService_TasksAddedAfterCompletionFinish()
    {
        using var rlq = new RateLimitingService(TimeSpan.Zero);

        bool wasRun = false;

        Task<bool> MyAction()
        {
            wasRun = true;
            return Task.FromResult(true);
        }

        var mockHandler = new MockHttpMessageHandler(MyAction, MyAction);
        var httpClient = new HttpClient(mockHandler);

        var task = rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);
        var res = await task;

        Assert.NotNull(res);
        Assert.Equal(TaskStatus.RanToCompletion, task.Status);
        Assert.True(wasRun);

        var res2 = await rlq.EnqueueHttpRequest(httpClient, MockHttpRequestMessage);
        Assert.NotNull(res2);
    }
}
