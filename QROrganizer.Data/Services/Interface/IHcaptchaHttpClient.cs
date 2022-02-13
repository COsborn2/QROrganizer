using System.Threading.Tasks;
using QROrganizer.Data.Services.Models;

namespace QROrganizer.Data.Services.Interface;

public interface IHcaptchaHttpClient
{
    Task<HCaptchaVerifyResponse> VerifyCaptcha(string captchaToken);
}
