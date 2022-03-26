using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.TestTools.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using QROrganizer.Data.Models;
using QROrganizer.Data.Policies;
using QROrganizer.Data.Tests.Util;
using QROrganizer.Data.Util;
using Xunit;

namespace QROrganizer.Data.Tests;

public class ClaimsPrincipalFactoryTests : IClassFixture<DatabaseFixture<AppDbContext>>
{
    private readonly DatabaseFixture<AppDbContext> _dbFixture;
    private readonly AutoMocker _autoMocker;

    public ClaimsPrincipalFactoryTests(DatabaseFixture<AppDbContext> dbFixture)
    {
        _dbFixture = dbFixture ?? throw new ArgumentNullException(nameof(dbFixture));
        _autoMocker = new AutoMocker();
        _autoMocker.Use(Options.Create(new IdentityOptions()));

        var mock = _autoMocker.GetMock<UserManager<ApplicationUser>>();
        mock.Setup(x => x.GetUserIdAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync((ApplicationUser user) => user.Id);
        mock.Setup(x => x.GetUserNameAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync((ApplicationUser user) => user.UserName);
    }

    private static ApplicationUser ApplicationUser => new()
    {
        UserName = "Person McPerson",
        Id = "1"
    };

    [Fact]
    public async Task GenerateClaimsAsync_ValidSubscriptionLevel()
    {
        ClaimsPrincipal? claimsPrincipal = null;
        var subscriptionFeature = new SubscriptionFeature
        {
            Name = Feature.BARCODE_LOOKUP.ToString(),
            IsEnabled = true
        };
        var subscriptionLevel = new SubscriptionLevel
        {
            Features = new[] { subscriptionFeature },
            SubscriptionName = "Basic Subscription"
        };

        await _dbFixture.PerformDatabaseOperation(async context =>
        {
            _autoMocker.Use(context);
            context.SubscriptionFeatures.Add(subscriptionFeature);
            context.SubscriptionLevels.Add(subscriptionLevel);
            await context.SaveChangesAsync();

            var sut = _autoMocker.CreateInstance<ClaimsPrincipalFactory>();
            var appUser = ApplicationUser;
            appUser.SubscriptionLevelId = subscriptionLevel.Id;
            claimsPrincipal = await sut.CreateAsync(appUser);
        });

        Assert.NotNull(claimsPrincipal);
        var claims = claimsPrincipal!.Claims.ToList();

        claims.ValidateClaim(subscriptionLevel.Id.ToString(), AppClaimsTypes.SubscriptionLevelId);
        claims.ValidateClaim(subscriptionLevel.SubscriptionName, AppClaimsTypes.SubscriptionLevelName);
        claims.ValidateClaim(Feature.BARCODE_LOOKUP.ToString(), AppClaimsTypes.ActiveFeature);
    }

    [Fact]
    public async Task GenerateClaimsAsync_SubscriptionNotFound()
    {
        ClaimsPrincipal? claimsPrincipal = null;

        await _dbFixture.PerformDatabaseOperation(async context =>
        {
            _autoMocker.Use(context);

            var sut = _autoMocker.CreateInstance<ClaimsPrincipalFactory>();
            var appUser = ApplicationUser;
            claimsPrincipal = await sut.CreateAsync(appUser);
        });

        Assert.NotNull(claimsPrincipal);
        var claims = claimsPrincipal!.Claims.ToList();

        Assert.Empty(claims.Where(x => x.Type == AppClaimsTypes.SubscriptionLevelId));
        Assert.Empty(claims.Where(x => x.Type == AppClaimsTypes.SubscriptionLevelName));
        Assert.Empty(claims.Where(x => x.Type == AppClaimsTypes.ActiveFeature));
    }
}
