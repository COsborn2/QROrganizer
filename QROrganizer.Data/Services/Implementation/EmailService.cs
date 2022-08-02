using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Util;
using SendGrid;

namespace QROrganizer.Data.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly HttpContextInfo _httpContextInfo;
    private readonly ILogger _logger;
    private readonly AppConfigSettings _appConfigSettings;
    private readonly Lazy<SendGridClient> _client;

    public EmailService(
        IOptions<AppConfigSettings> appSettings,
        HttpContextInfo httpContextInfo,
        ILogger<EmailService> logger)
    {
        if (appSettings is null) throw new ArgumentNullException(nameof(appSettings));
        _httpContextInfo = httpContextInfo ?? throw new ArgumentNullException(nameof(httpContextInfo));
        _logger = logger;
        _appConfigSettings = appSettings.Value;
        _client = new Lazy<SendGridClient>(() => new SendGridClient(appSettings.Value.SendGridApiKey));
    }

    public async Task<bool> SendConfirmationEmail(string confirmationCode, string userId, string toEmail)
    {
        if (!_appConfigSettings.SendEmails)
        {
            _logger.LogWarning("Skipping email send due to configuration!");
            return true;
        }

        var message = SendGridMessageHelpers.BuildEmailConfirmation(
            _httpContextInfo.BaseUrl,
            confirmationCode,
            userId);
        message.AddTo(toEmail);
        
        //sanity check..
        if (message.Personalizations.SelectMany(x => x.Tos).Count() > 1)
        {
            var exception =
                new InvalidOperationException("Attempted to send email to multiple users! Aborting and failing");
            _logger.LogError(exception, "Aborting email send due to multiple to users!");
        }
        
        var res = await _client.Value.SendEmailAsync(message);

        if (!res.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to send SendGrid email for user with email '{toEmail}'");
        }

        return res.IsSuccessStatusCode;
    }
}
