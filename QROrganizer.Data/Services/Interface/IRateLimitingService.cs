using System.Net.Http;
using System.Threading.Tasks;

namespace QROrganizer.Data.Services.Interface;

public interface IRateLimitingService
{
    Task<HttpResponseMessage> EnqueueHttpRequest(HttpClient httpClient, HttpRequestMessage httpRequestMessage);
}

public interface IRateLimitingService<out T> : IRateLimitingService
{ }
