using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Services.Models;

namespace QROrganizer.Data.Services.Implementation;

public class BarcodeSpiderHttpClient : IBarcodeSpiderHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IRateLimitingService<IBarcodeSpiderHttpClient> _rateLimitingService;
    private readonly AppDbContext _dbContext;

    public BarcodeSpiderHttpClient(
        HttpClient httpClient,
        IRateLimitingService<IBarcodeSpiderHttpClient> rateLimitingService,
        AppDbContext dbContext)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _rateLimitingService = rateLimitingService ?? throw new ArgumentNullException(nameof(rateLimitingService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task LookupUpcCode(string upcCode)
    {
// TODO: Check that feature is enabled
// TODO: Requeue if 429 response
// TODO: When calling this from controller - don't return actual response
// TODO: Consider having a in-queue table with pending queries -- something like Caller, MetaData, etc. -- if inqueue, don't request again
// TODO: On client side -- have list of "pending" requests -- update the request once it has been completed
// TODO: For feature toggle table add a column for feature enabled/disabled

        var urlSegment = "lookup" + QueryString.Create("upc", upcCode).ToUriComponent();
        var uri = new Uri(urlSegment, UriKind.Relative);
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        var res = await _rateLimitingService.EnqueueHttpRequest(_httpClient, httpRequestMessage);
        var lookupResponse = JsonConvert.DeserializeObject<BarcodeSpiderLookupResponse>(
            await res.Content.ReadAsStringAsync());
    }
}
