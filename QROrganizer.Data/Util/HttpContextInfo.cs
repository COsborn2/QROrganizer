using System;
using Microsoft.AspNetCore.Http;

namespace QROrganizer.Data.Util;

public class HttpContextInfo
{
    public HttpContextInfo(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        BaseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
    }

    /// <summary>
    /// Get BaseUrl of application
    /// </summary>
    public string BaseUrl { get; }
}
