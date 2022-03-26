using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce.DataAnnotations;
using QROrganizer.Data.Models;

namespace QROrganizer.Data.Services.Interface;

public interface IItemScanningService
{
    Task<Item> CreateItemForUpcCodeAndStartSearch(
        ClaimsPrincipal claimsPrincipal,
        string upcCode,
        int containerId);
}
