using System.Threading.Tasks;

namespace QROrganizer.Data.Services.Interface;

public interface IBarcodeSpiderHttpClient
{
    Task LookupUpcCode(string upcCode);
}
