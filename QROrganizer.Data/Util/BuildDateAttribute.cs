using System;
using System.Globalization;

namespace QROrganizer.Data.Util;

[AttributeUsage(AttributeTargets.Assembly)]
public class BuildDateAttribute : Attribute
{
    public DateTimeOffset DateTimeOffset { get; }

    public BuildDateAttribute(string date)
    {
        DateTimeOffset = DateTimeOffset.ParseExact(
            date,
            "yyyy-MM-ddTHH:mm:ss.sssZ",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None);
    }
}
