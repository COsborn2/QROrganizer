using System.Threading.Tasks;

namespace QROrganizer.Data.Services.Interface
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmail(string confirmationCode, string userId, string toEmail);
    }
}
