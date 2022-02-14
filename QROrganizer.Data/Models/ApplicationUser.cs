using IntelliTect.Coalesce.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QROrganizer.Data.Models;

[Create(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Delete(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Edit(Roles = Roles.Admin)]
[Read(Roles = Roles.Admin)]
public class ApplicationUser : IdentityUser
{ }
