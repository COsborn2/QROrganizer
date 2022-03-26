using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Models;
using QROrganizer.Data.Models;

namespace QROrganizer.Data.Services.Interface;

[Coalesce, Service]
public interface IItemScanningService
{
    [Execute(SecurityPermissionLevels.AllowAuthorized)]
    Task<ItemResult<Item>> CreateItemForUpcCodeAndStartSearch(
        ClaimsPrincipal claimsPrincipal,
        string upcCode,
        int containerId);
}
