using System.Collections.Generic;
using System.Linq;

namespace QROrganizer.Data
{
    public static class Roles
    {
        static Roles()
        {
            AllRolesNormalized = typeof(Roles).GetFields()
                .Where(x => x.Name != nameof(AllRolesNormalized))
                .Select(x => x.Name.ToUpperInvariant())
                .ToList();
        }

        public static ICollection<string> AllRolesNormalized;

        public const string Admin = "ADMIN";
    }
}
