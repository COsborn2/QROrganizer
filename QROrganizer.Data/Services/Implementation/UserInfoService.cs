using System.Security.Claims;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Implementation
{
    [Coalesce]
    [Service]
    public class UserInfoService
    {
        [Coalesce]
        [Execute(SecurityPermissionLevels.AllowAll)]
        public UserInfo GetUserInfo(ClaimsPrincipal claimsPrincipal)
        {
            return new UserInfo
            {
                Email = claimsPrincipal.Email(),
                Username = claimsPrincipal.Username(),
                Roles = claimsPrincipal.Roles()
            };
        }
    }
}
