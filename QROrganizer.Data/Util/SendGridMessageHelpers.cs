using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SendGrid.Helpers.Mail;

namespace QROrganizer.Data.Util;

public static class SendGridMessageHelpers
{
    public static readonly SendGridMessage MessageWithDoNotReplySender = new()
    {
        From = new EmailAddress
        {
            Email = "donotreply@cameronosborn.dev"
        }
    };

    public static SendGridMessage BuildEmailConfirmation(string baseUrl, string confirmationToken, string userId)
    {
        var message = MessageWithDoNotReplySender;
        message.TemplateId = SendGridTemplateIds.EmailConfirmation;

        var queryString = QueryString.Create(new List<KeyValuePair<string, StringValues>>
        {
            new("confirmationToken", HttpUtility.UrlEncode(confirmationToken)),
            new("userId", HttpUtility.UrlEncode(userId))
        });

        var url = new Uri(new Uri(baseUrl, UriKind.Absolute),
            new Uri("confirmemail", UriKind.Relative));
        url = new Uri(url, queryString.Value);
        message.SetTemplateData(new EmailConfirmationDynamicData
        {
            ConfirmationUrl = url.AbsoluteUri
        });

        return message;
    }
}
