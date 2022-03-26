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
    public partial class SubscriptionLevelDtoGen : GeneratedDto<QROrganizer.Data.Models.SubscriptionLevel>
    {
        public SubscriptionLevelDtoGen() { }

        private int? _Id;
        private string _SubscriptionName;
        private System.Collections.Generic.ICollection<QROrganizer.Web.Models.SubscriptionFeatureDtoGen> _Features;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string SubscriptionName
        {
            get => _SubscriptionName;
            set { _SubscriptionName = value; Changed(nameof(SubscriptionName)); }
        }
        public System.Collections.Generic.ICollection<QROrganizer.Web.Models.SubscriptionFeatureDtoGen> Features
        {
            get => _Features;
            set { _Features = value; Changed(nameof(Features)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.SubscriptionLevel obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.SubscriptionName = obj.SubscriptionName;
            var propValFeatures = obj.Features;
            if (propValFeatures != null && (tree == null || tree[nameof(this.Features)] != null))
            {
                this.Features = propValFeatures
                    .OrderBy(f => f.Name)
                    .Select(f => f.MapToDto<QROrganizer.Data.Models.SubscriptionFeature, SubscriptionFeatureDtoGen>(context, tree?[nameof(this.Features)])).ToList();
            }
            else if (propValFeatures == null && tree?[nameof(this.Features)] != null)
            {
                this.Features = new SubscriptionFeatureDtoGen[0];
            }

        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.SubscriptionLevel entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(SubscriptionName))) entity.SubscriptionName = SubscriptionName;
        }
    }
}
