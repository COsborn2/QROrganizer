using System.Collections.Generic;
using System.Threading.Tasks;
using QROrganizer.Data.Models;

namespace QROrganizer.Data.Services.Interface;

public interface IUpcLookupService
{
    Task<ICollection<ItemBarcodeInformation>> LookupUpcCode(string upcCode);
}
