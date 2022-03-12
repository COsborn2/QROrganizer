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
    public partial class ContainerDtoGen : GeneratedDto<QROrganizer.Data.Models.Container>
    {
        public ContainerDtoGen() { }

        private int? _Id;
        private string _ContainerName;
        private string _UserId;
        private System.Collections.Generic.ICollection<QROrganizer.Web.Models.ItemDtoGen> _Items;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string ContainerName
        {
            get => _ContainerName;
            set { _ContainerName = value; Changed(nameof(ContainerName)); }
        }
        public string UserId
        {
            get => _UserId;
            set { _UserId = value; Changed(nameof(UserId)); }
        }
        public System.Collections.Generic.ICollection<QROrganizer.Web.Models.ItemDtoGen> Items
        {
            get => _Items;
            set { _Items = value; Changed(nameof(Items)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.Container obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.ContainerName = obj.ContainerName;
            this.UserId = obj.UserId;
            var propValItems = obj.Items;
            if (propValItems != null && (tree == null || tree[nameof(this.Items)] != null))
            {
                this.Items = propValItems
                    .OrderBy(f => f.Name)
                    .Select(f => f.MapToDto<QROrganizer.Data.Models.Item, ItemDtoGen>(context, tree?[nameof(this.Items)])).ToList();
            }
            else if (propValItems == null && tree?[nameof(this.Items)] != null)
            {
                this.Items = new ItemDtoGen[0];
            }

        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.Container entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(ContainerName))) entity.ContainerName = ContainerName;
            if (ShouldMapTo(nameof(UserId))) entity.UserId = UserId;
        }
    }
}
