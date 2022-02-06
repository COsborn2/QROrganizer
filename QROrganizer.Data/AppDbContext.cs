using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QROrganizer.Data.Models;
using IntelliTect.Coalesce;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QROrganizer.Data
{
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
            SaveChanges();
        }
    }
}
