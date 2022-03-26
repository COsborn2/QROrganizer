using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QROrganizer.Data.Models;
using QROrganizer.Data.Policies;

namespace QROrganizer.Data.Util;

public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    private readonly AppDbContext _context;

    public ClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options,
        AppDbContext context)
        : base(userManager, roleManager, options)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user);

        // Get SubscriptionLevel from database
        var subscriptionLevel = _context.SubscriptionLevels
            .Include(x => x.Features)
            .SingleOrDefault(x => x.Id == user.SubscriptionLevelId);
        if (subscriptionLevel is null)
        {
            return claimsIdentity;
        }

        claimsIdentity.AddClaim(new Claim(AppClaimsTypes.SubscriptionLevelId, subscriptionLevel.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(AppClaimsTypes.SubscriptionLevelName, subscriptionLevel.SubscriptionName));

        foreach (var feature in subscriptionLevel.Features)
        {
            var claimType = feature.IsEnabled ? AppClaimsTypes.ActiveFeature : AppClaimsTypes.InactiveFeature;
            claimsIdentity.AddClaim(new Claim(claimType, feature.Name));
        }

        return claimsIdentity;
    }
}
