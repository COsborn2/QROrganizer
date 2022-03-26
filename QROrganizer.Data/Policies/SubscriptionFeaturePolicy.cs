using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace QROrganizer.Data.Policies;

public class SubscriptionFeaturePolicy : AuthorizationHandler<SubscriptionFeatureRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SubscriptionFeatureRequirement requirement)
    {
        var hasClaim = context.User.Claims
            .Where(x => x.Type == AppClaimsTypes.ActiveFeature)
            .Any(x => x.Value == requirement.Feature.ToString());

        if (hasClaim)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
