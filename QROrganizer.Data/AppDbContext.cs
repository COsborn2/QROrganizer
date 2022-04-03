using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QROrganizer.Data.Models;
using IntelliTect.Coalesce;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using QROrganizer.Data.Util;

namespace QROrganizer.Data;

[Coalesce]
public class AppDbContext
    : IdentityDbContext<
        ApplicationUser,
        IdentityRole,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
{
    public DbSet<RestrictedAccessCode> AccessCodes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<ItemBarcodeInformation> ItemBarcodeInformation { get; set; }
    public DbSet<SubscriptionLevel> SubscriptionLevels { get; set; }
    public DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Remove cascading deletes.
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder.Entity<ItemBarcodeInformation>()
            .HasIndex(x => x.UpcCode)
            .IsUnique(false);

        builder.Entity<Item>()
            .HasOne(x => x.ItemBarcodeInformation)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.UpcCode)
            .HasPrincipalKey(x => x.UpcCode)
            .IsRequired(false);
    }

    /// <summary>
    /// Migrates the database and sets up items that need to be set up from scratch.
    /// </summary>
    public void Initialize()
    {
        try
        {
            Database.Migrate();
        }
        catch (InvalidOperationException e) when (e.Message == "No service for type 'Microsoft.EntityFrameworkCore.Migrations.IMigrator' has been registered.")
        {
            // this exception is expected when using an InMemory database
        }

        var roles = Data.Roles.AllRolesNormalized;
        var presentRoles = Roles
            .Where(x => roles.Contains(x.NormalizedName));
        var rolesMissing = roles
            .Where(x => !presentRoles.Select(xi => xi.NormalizedName).Contains(x))
            .Select(x => new IdentityRole
            {
                Name = x,
                NormalizedName = x.ToUpperInvariant()
            });

        Roles.AddRange(rolesMissing);

        var features = typeof(Feature).GetValues();
        var presentFeatures = SubscriptionFeatures
            .Where(x => features.Contains(x.Name));
        var featuresMissing = features
            .Where(x => !presentFeatures.Select(xi => xi.Name).Contains(x))
            .Select(x => new SubscriptionFeature
            {
                Name = x,
                IsEnabled = true
            });

        SubscriptionFeatures.AddRange(featuresMissing);

        SaveChanges();
    }
}
