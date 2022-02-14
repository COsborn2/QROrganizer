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
    public partial class RestrictedAccessCodeDtoGen : GeneratedDto<QROrganizer.Data.Models.RestrictedAccessCode>
    {
        public RestrictedAccessCodeDtoGen() { }

        private int? _Id;
        private string _AccessCode;
        private int? _NumberOfUsesRemaining;
        private bool? _IsLimitedKey;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string AccessCode
        {
            get => _AccessCode;
            set { _AccessCode = value; Changed(nameof(AccessCode)); }
        }
        public int? NumberOfUsesRemaining
        {
            get => _NumberOfUsesRemaining;
            set { _NumberOfUsesRemaining = value; Changed(nameof(NumberOfUsesRemaining)); }
        }
        public bool? IsLimitedKey
        {
            get => _IsLimitedKey;
            set { _IsLimitedKey = value; Changed(nameof(IsLimitedKey)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.RestrictedAccessCode obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.AccessCode = obj.AccessCode;
            this.NumberOfUsesRemaining = obj.NumberOfUsesRemaining;
            this.IsLimitedKey = obj.IsLimitedKey;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.RestrictedAccessCode entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(AccessCode))) entity.AccessCode = AccessCode;
            if (ShouldMapTo(nameof(NumberOfUsesRemaining))) entity.NumberOfUsesRemaining = (NumberOfUsesRemaining ?? entity.NumberOfUsesRemaining);
            if (ShouldMapTo(nameof(IsLimitedKey))) entity.IsLimitedKey = (IsLimitedKey ?? entity.IsLimitedKey);
        }
    }
}
