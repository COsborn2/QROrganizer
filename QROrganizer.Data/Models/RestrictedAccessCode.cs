using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;

namespace QROrganizer.Data.Models;

[Create(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Delete(Roles = Roles.Admin)]
[Edit(Roles = Roles.Admin)]
[Read(Roles = Roles.Admin)]
public class RestrictedAccessCode
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string AccessCode { get; set; }

    public int NumberOfUsesRemaining { get; set; }

    public bool IsLimitedKey { get; set; }

    [Coalesce]
    [Execute(Roles = Roles.Admin)]
    public static async Task CreateUnlimitedUseAccessCode([Inject] AppDbContext db)
    {
        var accessCode = new RestrictedAccessCode
        {
            AccessCode = Guid.NewGuid().ToString("N"),
            NumberOfUsesRemaining = 0,
            IsLimitedKey = false
        };

        db.AccessCodes.Add(accessCode);
        await db.SaveChangesAsync();
    }

    [Coalesce]
    [Execute(Roles = Roles.Admin)]
    public static async Task CreateAccessCode([Inject] AppDbContext db, int numberOfUses)
    {
        var accessCode = new RestrictedAccessCode
        {
            AccessCode = Guid.NewGuid().ToString("N"),
            NumberOfUsesRemaining = numberOfUses,
            IsLimitedKey = true
        };

        db.AccessCodes.Add(accessCode);
        await db.SaveChangesAsync();
    }
}
