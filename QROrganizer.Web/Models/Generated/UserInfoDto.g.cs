using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QROrganizer.Web.Models
{
    public partial class UserInfoDtoGen : GeneratedDto<QROrganizer.Data.Services.Models.UserInfo>
    {
        public UserInfoDtoGen() { }

        private string _Email;
        private string _Username;
        private string _SubscriptionName;
        private System.Collections.Generic.ICollection<string> _Roles;
        private System.Collections.Generic.ICollection<string> _Features;

        public string Email
        {
            get => _Email;
            set { _Email = value; Changed(nameof(Email)); }
        }
        public string Username
        {
            get => _Username;
            set { _Username = value; Changed(nameof(Username)); }
        }
        public string SubscriptionName
        {
            get => _SubscriptionName;
            set { _SubscriptionName = value; Changed(nameof(SubscriptionName)); }
        }
        public System.Collections.Generic.ICollection<string> Roles
        {
            get => _Roles;
            set { _Roles = value; Changed(nameof(Roles)); }
        }
        public System.Collections.Generic.ICollection<string> Features
        {
            get => _Features;
            set { _Features = value; Changed(nameof(Features)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Services.Models.UserInfo obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Email = obj.Email;
            this.Username = obj.Username;
            this.SubscriptionName = obj.SubscriptionName;
            this.Roles = obj.Roles;
            this.Features = obj.Features;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Services.Models.UserInfo entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Email))) entity.Email = Email;
            if (ShouldMapTo(nameof(Username))) entity.Username = Username;
            if (ShouldMapTo(nameof(SubscriptionName))) entity.SubscriptionName = SubscriptionName;
            if (ShouldMapTo(nameof(Roles))) entity.Roles = Roles;
            if (ShouldMapTo(nameof(Features))) entity.Features = Features;
        }
    }
}
