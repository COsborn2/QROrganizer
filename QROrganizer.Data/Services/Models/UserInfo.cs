using System.Collections.Generic;
using System.Security.Claims;
using QROrganizer.Data.Util;

namespace QROrganizer.Data.Services.Models
{
    public class UserInfo
    {
        public UserInfo()
        { }

        public UserInfo(ClaimsPrincipal cp)
        {
            Email = cp.Email();
            Username = cp.Username();
            Roles = cp.Roles();
            ActiveFeatures = cp.ActiveFeatures();
            InactiveFeatures = cp.InactiveFeatures();
            SubscriptionName = cp.SubscriptionLevel();
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public string SubscriptionName { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<string> ActiveFeatures { get; set; }
        public ICollection<string> InactiveFeatures { get; set; }
    }
}
