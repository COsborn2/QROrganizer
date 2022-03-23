using System;
using System.Collections.Generic;
using System.Linq;

namespace QROrganizer.Data.Util;

public static class EnumExtensions
{
    public static IEnumerable<string> GetFlags(this Enum e)
    {
        return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag).Select(x => x.ToString());
    }
}
