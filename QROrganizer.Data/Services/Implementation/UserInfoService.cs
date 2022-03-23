using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Models;
using Microsoft.AspNetCore.Identity;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Models;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Implementation
{
    [Coalesce]
    [Service]
    public class UserInfoService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInfoService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [Coalesce]
        [Execute(SecurityPermissionLevels.AllowAll)]
        public UserInfo GetUserInfo(ClaimsPrincipal claimsPrincipal)
        {
            return new UserInfo
            {
                Email = claimsPrincipal.Email(),
                Username = claimsPrincipal.Username(),
                Roles = claimsPrincipal.Roles(),
                Features = claimsPrincipal.Features(),
                SubscriptionName = claimsPrincipal.SubscriptionLevel()
            };
        }

        [Coalesce]
        [Execute(SecurityPermissionLevels.AllowAll)]
        public async Task<ItemResult> ConfirmEmail(string userId, string confirmationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return new ItemResult("User could not be found with that email address. Visit SignUp to start!");
            }

            if (user.EmailConfirmed)
            {
                return new ItemResult("Email is already verified");
            }

            var res = await _userManager.ConfirmEmailAsync(user, confirmationToken);

            return !res.Succeeded
                ? new ItemResult(res.Errors.Select(x => x.Description).First())
                : new ItemResult<bool>(true);
        }
    }
}
