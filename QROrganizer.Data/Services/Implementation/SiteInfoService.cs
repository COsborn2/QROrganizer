using System;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Implementation;

public class SiteInfoService : ISiteInfoService
{
    private readonly SiteInfoDto _siteInfo;

    public SiteInfoService(IOptions<AppConfigSettings> appConfigSettings)
    {
        _siteInfo = new SiteInfoDto
        {
            BuildDate = SiteInfo.GetBuildDate(),
            RestrictedEnvironment = appConfigSettings.Value.RestrictedEnvironment
        };
    }

    public SiteInfoDto GetSiteInfo()
    {
        return _siteInfo;
    }
}
