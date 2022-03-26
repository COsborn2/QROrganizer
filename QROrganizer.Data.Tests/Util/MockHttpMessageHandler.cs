using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QROrganizer.Data.Tests.Util;

public class MockHttpMessageHandler : DelegatingHandler
{
    private readonly Queue<Func<Task>> _funcQueue;

    public MockHttpMessageHandler(params Func<Task>[] funcs)
    {
        var list = funcs ?? throw new ArgumentNullException(nameof(funcs));
        _funcQueue = new Queue<Func<Task>>(list);
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await _funcQueue.Dequeue()();
        return new HttpResponseMessage();
    }
}
