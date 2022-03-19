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

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SubscriptionFeatureRequirement requirement)
    {
        // TODO: Implement
        return Task.CompletedTask;
    }
}
