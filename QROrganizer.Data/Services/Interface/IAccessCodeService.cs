using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;

namespace QROrganizer.Data.Services.Interface;

[Coalesce, Service]
public interface IAccessCodeService
{
    [Execute(SecurityPermissionLevels.DenyAll)]
    Task<ErrorResponse> ValidateAndUseAccessCode(string code);

    [Execute(SecurityPermissionLevels.AllowAll)]
    bool IsAccessCodeValid(string code);
}
