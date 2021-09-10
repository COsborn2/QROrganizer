using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Utilities;

namespace QROrganizer.Data.Services
{
    [Coalesce, Service]
    public class UserService
    {
        [Coalesce]
        public UserInfo UserInfo(ClaimsPrincipal user)
        {
            return new UserInfo
            {
                UserName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Roles = user.GetRoles().ToList()
            };
        }
    }

    public class UserInfo
    {
        public string UserName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
