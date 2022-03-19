using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class RateLimitingService : RateLimitingService<object>
{
    public RateLimitingService(TimeSpan rateLimitInterval) : base(rateLimitInterval)
    { }
}

public class RateLimitingService<T> : IRateLimitingService<T>, IDisposable
{
    private readonly BlockingCollection<Func<Task>> _queue = new();
    private readonly object _locker = new();
    private readonly TimeSpan _rateLimit;
    private Task<Task> _drivingTask;
    private readonly SemaphoreSlim _semaphore;

    public RateLimitingService(TimeSpan rateLimitInterval)
    {
        _rateLimit = rateLimitInterval;
        _semaphore = new SemaphoreSlim(1);
    }

    public Task<HttpResponseMessage> EnqueueHttpRequest(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage)
    {
        var taskResult = new TaskCompletionSource<HttpResponseMessage>();
        var task = taskResult.Task;

        async Task TaskWrapper()
        {
            try
            {
                var res = await httpClient.SendAsync(httpRequestMessage);
                taskResult.SetResult(res);
            }
            catch (Exception e)
            {
                taskResult.SetException(e);
            }
        }

        _queue.TryAdd(TaskWrapper);
        StartIfInactive();

        return task;
    }

    private void StartIfInactive()
    {
        if (_drivingTask?.Status == TaskStatus.Running || _semaphore.CurrentCount < 1) return;
        lock (_locker)
        {
            if (_drivingTask?.Status != TaskStatus.Running && _semaphore.CurrentCount >= 1)
            {
                _semaphore.Wait();
                _drivingTask = Task.Factory.StartNew(RunTasks, TaskCreationOptions.LongRunning);
            }
        }
    }

    private async Task RunTasks()
    {
        foreach (var func in _queue.GetConsumingEnumerable())
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                await func();
                stopWatch.Stop();

                var remainingTime = _rateLimit - stopWatch.Elapsed;
                if (remainingTime > TimeSpan.Zero)
                {
                    await Task.Delay(remainingTime);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _semaphore.Release();
    }

    private bool _disposed;
    public void Dispose() => Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _queue?.CompleteAdding();
            _queue?.Dispose();
        }

        _disposed = true;
    }

}
