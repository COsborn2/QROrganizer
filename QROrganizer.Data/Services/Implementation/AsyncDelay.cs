using System;
using System.Threading.Tasks;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class AsyncDelay : IAsyncDelay
{
    public Task Delay(TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan);
    }
}