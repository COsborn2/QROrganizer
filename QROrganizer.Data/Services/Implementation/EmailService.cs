using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Util;
using SendGrid;

namespace QROrganizer.Data.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly HttpContextInfo _httpContextInfo;
    private readonly SendGridClient _client;

    public EmailService(IOptions<AppConfigSettings> appSettings, HttpContextInfo httpContextInfo)
    {
        if (appSettings is null) throw new ArgumentNullException(nameof(appSettings));
        _httpContextInfo = httpContextInfo ?? throw new ArgumentNullException(nameof(httpContextInfo));
        _client = new SendGridClient(appSettings.Value.SendGridApiKey);
    }

    public async Task<bool> SendEmailConfirmationEmail(string confirmationCode, string userId, string toEmail)
    {
        // TODO: Use response to log failure/success
        var message = SendGridMessageHelpers.BuildEmailConfirmation(
            _httpContextInfo.BaseUrl,
            confirmationCode,
            userId);
        message.AddTo(toEmail);
        var res = await _client.SendEmailAsync(message);

        return res.IsSuccessStatusCode;
    }
}
