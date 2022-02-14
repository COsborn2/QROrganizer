using System;
using System.Reflection;

namespace QROrganizer.Data.Util;

public static class SiteInfo
{
    public static DateTimeOffset GetBuildDate()
    {
        return Assembly.GetExecutingAssembly().GetCustomAttribute<BuildDateAttribute>()!.DateTimeOffset;
    }
}
