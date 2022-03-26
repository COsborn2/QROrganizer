using Microsoft.AspNetCore.Authorization;

namespace QROrganizer.Data.Policies;

public class SubscriptionFeatureRequirement : IAuthorizationRequirement
{
    public SubscriptionFeatureRequirement(Feature feature)
    {
        Feature = feature;
    }

    public Feature Feature { get; }
}
