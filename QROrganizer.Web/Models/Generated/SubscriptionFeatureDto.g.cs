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
    public partial class SubscriptionFeatureDtoGen : GeneratedDto<QROrganizer.Data.Models.SubscriptionFeature>
    {
        public SubscriptionFeatureDtoGen() { }

        private int? _Id;
        private string _Name;
        private bool? _IsEnabled;
        private System.Collections.Generic.ICollection<QROrganizer.Web.Models.SubscriptionLevelDtoGen> _SubscriptionLevels;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string Name
        {
            get => _Name;
            set { _Name = value; Changed(nameof(Name)); }
        }
        public bool? IsEnabled
        {
            get => _IsEnabled;
            set { _IsEnabled = value; Changed(nameof(IsEnabled)); }
        }
        public System.Collections.Generic.ICollection<QROrganizer.Web.Models.SubscriptionLevelDtoGen> SubscriptionLevels
        {
            get => _SubscriptionLevels;
            set { _SubscriptionLevels = value; Changed(nameof(SubscriptionLevels)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.SubscriptionFeature obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.Name = obj.Name;
            this.IsEnabled = obj.IsEnabled;
            var propValSubscriptionLevels = obj.SubscriptionLevels;
            if (propValSubscriptionLevels != null && (tree == null || tree[nameof(this.SubscriptionLevels)] != null))
            {
                this.SubscriptionLevels = propValSubscriptionLevels
                    .OrderBy(f => f.Id)
                    .Select(f => f.MapToDto<QROrganizer.Data.Models.SubscriptionLevel, SubscriptionLevelDtoGen>(context, tree?[nameof(this.SubscriptionLevels)])).ToList();
            }
            else if (propValSubscriptionLevels == null && tree?[nameof(this.SubscriptionLevels)] != null)
            {
                this.SubscriptionLevels = new SubscriptionLevelDtoGen[0];
            }

        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.SubscriptionFeature entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(IsEnabled))) entity.IsEnabled = (IsEnabled ?? entity.IsEnabled);
        }
    }
}
