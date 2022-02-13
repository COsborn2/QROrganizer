using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Services.Models;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Implementation;

public class HcaptchaHttpClient : IHcaptchaHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AppConfigSettings _appSettings;

    public HcaptchaHttpClient(HttpClient httpClient, IOptions<AppConfigSettings> appSettings)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public async Task<HCaptchaVerifyResponse> VerifyCaptcha(string captchaToken)
    {
        var body = new List<KeyValuePair<string, string>>
        {
            new("response", captchaToken),
            new("secret", _appSettings.HCaptchaSecret)
        };

        var uri = new Uri("siteverify", UriKind.Relative);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new FormUrlEncodedContent(body)
        };
        var res = await _httpClient.SendAsync(requestMessage);

        res.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<HCaptchaVerifyResponse>(await res.Content.ReadAsStringAsync());
    }
}
