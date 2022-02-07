using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using QROrganizer.Data.Models;

namespace QROrganizer.Data.Services.Interface;

[Coalesce, Service]
public interface IAccessCodeService
{
    [Execute(SecurityPermissionLevels.DenyAll)]
    Task<ErrorResponse> ValidateAndUseAccessCode(string code);

    [Execute(SecurityPermissionLevels.DenyAll)]
    ErrorResponse CheckAccessCodeValidAndRetrieve(string code, out RestrictedAccessCode accessCode);

    [Execute(SecurityPermissionLevels.AllowAll)]
    bool IsAccessCodeValid(string code);
}
