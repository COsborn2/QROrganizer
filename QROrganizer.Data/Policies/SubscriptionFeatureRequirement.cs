using Microsoft.AspNetCore.Authorization;

namespace QROrganizer.Data.Policies;

public class SubscriptionFeatureRequirement : IAuthorizationRequirement
{
    public SubscriptionFeatureRequirement(SubscriptionFeature subscriptionFeature)
    {
        SubscriptionFeature = subscriptionFeature;
    }

    public SubscriptionFeature SubscriptionFeature { get; }
}
