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
    public partial class SiteInfoDtoDtoGen : GeneratedDto<QROrganizer.Data.Util.SiteInfoDto>
    {
        public SiteInfoDtoDtoGen() { }

        private System.DateTimeOffset? _BuildDate;
        private bool? _RestrictedEnvironment;

        public System.DateTimeOffset? BuildDate
        {
            get => _BuildDate;
            set { _BuildDate = value; Changed(nameof(BuildDate)); }
        }
        public bool? RestrictedEnvironment
        {
            get => _RestrictedEnvironment;
            set { _RestrictedEnvironment = value; Changed(nameof(RestrictedEnvironment)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Util.SiteInfoDto obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.BuildDate = obj.BuildDate;
            this.RestrictedEnvironment = obj.RestrictedEnvironment;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Util.SiteInfoDto entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(BuildDate))) entity.BuildDate = (BuildDate ?? entity.BuildDate);
            if (ShouldMapTo(nameof(RestrictedEnvironment))) entity.RestrictedEnvironment = (RestrictedEnvironment ?? entity.RestrictedEnvironment);
        }
    }
}
