using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce.Models;
using Microsoft.AspNetCore.Authorization;

namespace QROrganizer.Data.Policies;

// ReSharper disable once InconsistentNaming
public static class IAuthorizationServiceExtensions
{
    public static async Task<ItemResult<T>> RequireFeaturePermission<T>(
        this IAuthorizationService authorizationService,
        ClaimsPrincipal cp,
        Feature feature)
    {
        var featureString = feature.ToString();

        var authorized = await authorizationService.AuthorizeAsync(
            cp,
            featureString);

        return authorized.Succeeded
            ? null
            : new ItemResult<T>($"Unauthorized. Required Subscription Feature '{featureString}'");
    }
}
