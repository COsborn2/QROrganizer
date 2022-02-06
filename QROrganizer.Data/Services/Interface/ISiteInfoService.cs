using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Interface;

[Coalesce, Service]
public interface ISiteInfoService
{
    [Execute(SecurityPermissionLevels.AllowAll)]
    SiteInfoDto GetSiteInfo();
}
