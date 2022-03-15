using System;
using System.Net.Http;
using System.Threading.Tasks;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class BarcodeSpiderHttpClient : IBarcodeSpiderHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IRateLimitingService<IBarcodeSpiderHttpClient> _rateLimitingService;

    public BarcodeSpiderHttpClient(
        HttpClient httpClient,
        IRateLimitingService<IBarcodeSpiderHttpClient> rateLimitingService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _rateLimitingService = rateLimitingService ?? throw new ArgumentNullException(nameof(rateLimitingService));
    }
}
