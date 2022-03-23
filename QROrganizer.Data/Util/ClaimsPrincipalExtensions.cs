using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using QROrganizer.Data.Policies;

namespace QROrganizer.Data.Util;

public static class ClaimsPrincipalExtensions
{
    private static ICollection<string> GetClaimsOfType(this ClaimsPrincipal claimsPrincipal, string claimType)
        => claimsPrincipal.Claims.Where(x => x.Type == claimType)
            .Select(x => x.Value)
            .ToList();

    public static string Email(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.Email);

    public static string Username(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.Name);

    public static ICollection<string> Roles(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetClaimsOfType(ClaimTypes.Role);

    public static ICollection<string> Features(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetClaimsOfType(AppClaimsTypes.Feature);

    public static string SubscriptionLevel(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(AppClaimsTypes.SubscriptionLevelName);
}
