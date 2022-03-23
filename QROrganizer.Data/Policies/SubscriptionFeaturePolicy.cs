using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace QROrganizer.Data.Policies;

public class SubscriptionFeaturePolicy : AuthorizationHandler<SubscriptionFeatureRequirement>
{
    private readonly SubscriptionFeature _subscriptionFeature;

    public SubscriptionFeaturePolicy(SubscriptionFeature subscriptionFeature)
    {
        _subscriptionFeature = subscriptionFeature;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SubscriptionFeatureRequirement requirement)
    {
        var hasClaim = context.User.Claims.Where(x => x.Type == AppClaimsTypes.Feature)
            .Any(x => x.Value == _subscriptionFeature.ToString());

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
