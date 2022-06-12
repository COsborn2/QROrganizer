using System;
using System.Threading.Tasks;

namespace QROrganizer.Data.Services.Interface;

public interface IAsyncDelay
{
    Task Delay(TimeSpan timeSpan);
}