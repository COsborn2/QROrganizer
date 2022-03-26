using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using QROrganizer.Data.Policies;
using Xunit;

namespace QROrganizer.Data.Tests;

public class SubscriptionFeaturePolicyTests
{
    private ClaimsPrincipal GetClaimsPrincipalWithClaim(Claim claim) => new(new ClaimsIdentity(new[] { claim }));

    [Fact]
    public async Task HandleRequirementAsync_UserHasClaim()
    {
        var requiredFeature = Feature.BARCODE_LOOKUP;
        var sut = new SubscriptionFeaturePolicy(requiredFeature);

        var cp = GetClaimsPrincipalWithClaim(
            new Claim(AppClaimsTypes.ActiveFeature,
            requiredFeature.ToString()));
        var context = new AuthorizationHandlerContext(
            new[] { new SubscriptionFeatureRequirement(requiredFeature) },
            cp,
            null);
        await sut.HandleAsync(context);

        Assert.True(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleRequirementAsync_UserMissingClaim()
    {
        var requiredFeature = Feature.BARCODE_LOOKUP;
        var sut = new SubscriptionFeaturePolicy(requiredFeature);

        var cp = new ClaimsPrincipal();
        var context = new AuthorizationHandlerContext(
            new[] { new SubscriptionFeatureRequirement(requiredFeature) },
            cp,
            null);
        await sut.HandleAsync(context);

        Assert.True(context.HasFailed);
    }
}
