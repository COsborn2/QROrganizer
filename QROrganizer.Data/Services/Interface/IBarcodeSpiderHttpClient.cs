using System.Threading.Tasks;
using QROrganizer.Data.Services.Models;

namespace QROrganizer.Data.Services.Interface;

public interface IBarcodeSpiderHttpClient
{
    Task<BarcodeSpiderLookupResponse> LookupUpcCode(string upcCode);
}
