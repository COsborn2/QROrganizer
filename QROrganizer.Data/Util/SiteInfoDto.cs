using System;

namespace QROrganizer.Data.Util;

public class SiteInfoDto
{
    public DateTimeOffset BuildDate { get; set; }
    public bool RestrictedEnvironment { get; set; }
}
