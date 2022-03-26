using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Services.Models;

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

    public async Task<BarcodeSpiderLookupResponse> LookupUpcCode(string upcCode)
    {
        var urlSegment = "lookup" + QueryString.Create("upc", upcCode).ToUriComponent();
        var uri = new Uri(urlSegment, UriKind.Relative);
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        var res = await _rateLimitingService.EnqueueHttpRequest(_httpClient, httpRequestMessage);
        return JsonConvert.DeserializeObject<BarcodeSpiderLookupResponse>(
            await res.Content.ReadAsStringAsync());
    }
}
