using System;
using System.Linq;
using System.Web;
using QROrganizer.Data.Util;
using Xunit;

namespace QROrganizer.Data.Tests;

public class SendGridMessageHelpersTests
{
    [Fact]
    public void BuildEmailConfirmation_UrlEncoded()
    {
        var baseUrl = "https://testsite.com";
        var confirmationToken = "confirm/code";
        var userId = Guid.NewGuid();
        var message = SendGridMessageHelpers.BuildEmailConfirmation(
            baseUrl,
            confirmationToken,
            userId.ToString());

        var templateData = (EmailConfirmationDynamicData) message.Personalizations.Single().TemplateData;
        var expectedUrl = $"{baseUrl}/confirmemail" +
                          $"?confirmationToken={HttpUtility.UrlEncode(confirmationToken)}" +
                          $"&userId={userId.ToString()}";
        Assert.Equal(expectedUrl.ToLowerInvariant(), templateData.ConfirmationUrl.ToLowerInvariant());
    }
}
