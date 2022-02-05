using System.Collections.Generic;

namespace QROrganizer.Data.Services.Implementation
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
