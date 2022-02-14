using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QROrganizer.Data.Util
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Email(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        public static string Username(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Name);

        public static ICollection<string> Roles(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();
    }
}
