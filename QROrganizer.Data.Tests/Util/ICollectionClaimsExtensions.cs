using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace QROrganizer.Data.Tests.Util;

// ReSharper disable once InconsistentNaming
public static class ICollectionClaimsExtensions
{
    public static void ValidateClaim(this ICollection<Claim> claims, string expected, string claim)
    {
        Assert.Equal(expected, claims.Single(x => x.Type == claim).Value);
    }
}
