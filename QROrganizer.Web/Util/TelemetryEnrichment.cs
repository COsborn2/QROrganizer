using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using QROrganizer.Data.Util;

namespace QROrganizer.Web.Util;

public class TelemetryEnrichment : TelemetryInitializerBase
{
    public TelemetryEnrichment(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override void OnInitializeTelemetry(HttpContext platformContext, RequestTelemetry requestTelemetry, ITelemetry telemetry)
    {
        telemetry.Context.User.AuthenticatedUserId = platformContext.User.Username();
    }
}
