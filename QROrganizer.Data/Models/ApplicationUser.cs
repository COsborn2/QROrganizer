using IntelliTect.Coalesce.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QROrganizer.Data.Models
{
    [Create(PermissionLevel = SecurityPermissionLevels.DenyAll)]
    [Delete(PermissionLevel = SecurityPermissionLevels.DenyAll)]
    [Edit(PermissionLevel = SecurityPermissionLevels.DenyAll)]
    [Read(PermissionLevel = SecurityPermissionLevels.DenyAll)]
    public class ApplicationUser : IdentityUser
    { }
}
