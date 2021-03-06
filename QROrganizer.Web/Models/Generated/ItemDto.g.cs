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
    public partial class ItemDtoGen : GeneratedDto<QROrganizer.Data.Models.Item>
    {
        public ItemDtoGen() { }

        private int? _Id;
        private string _UpcCode;
        private QROrganizer.Web.Models.ItemBarcodeInformationDtoGen _ItemBarcodeInformation;
        private string _Name;
        private int? _Quantity;
        private string _UserId;
        private int? _ContainerId;
        private QROrganizer.Web.Models.ContainerDtoGen _Container;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string UpcCode
        {
            get => _UpcCode;
            set { _UpcCode = value; Changed(nameof(UpcCode)); }
        }
        public QROrganizer.Web.Models.ItemBarcodeInformationDtoGen ItemBarcodeInformation
        {
            get => _ItemBarcodeInformation;
            set { _ItemBarcodeInformation = value; Changed(nameof(ItemBarcodeInformation)); }
        }
        public string Name
        {
            get => _Name;
            set { _Name = value; Changed(nameof(Name)); }
        }
        public int? Quantity
        {
            get => _Quantity;
            set { _Quantity = value; Changed(nameof(Quantity)); }
        }
        public string UserId
        {
            get => _UserId;
            set { _UserId = value; Changed(nameof(UserId)); }
        }
        public int? ContainerId
        {
            get => _ContainerId;
            set { _ContainerId = value; Changed(nameof(ContainerId)); }
        }
        public QROrganizer.Web.Models.ContainerDtoGen Container
        {
            get => _Container;
            set { _Container = value; Changed(nameof(Container)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.Item obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.UpcCode = obj.UpcCode;
            this.Name = obj.Name;
            this.Quantity = obj.Quantity;
            this.UserId = obj.UserId;
            this.ContainerId = obj.ContainerId;
            if (tree == null || tree[nameof(this.ItemBarcodeInformation)] != null)
                this.ItemBarcodeInformation = obj.ItemBarcodeInformation.MapToDto<QROrganizer.Data.Models.ItemBarcodeInformation, ItemBarcodeInformationDtoGen>(context, tree?[nameof(this.ItemBarcodeInformation)]);

            if (tree == null || tree[nameof(this.Container)] != null)
                this.Container = obj.Container.MapToDto<QROrganizer.Data.Models.Container, ContainerDtoGen>(context, tree?[nameof(this.Container)]);

        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.Item entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(UpcCode))) entity.UpcCode = UpcCode;
            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(Quantity))) entity.Quantity = (Quantity ?? entity.Quantity);
            if (ShouldMapTo(nameof(UserId))) entity.UserId = UserId;
            if (ShouldMapTo(nameof(ContainerId))) entity.ContainerId = ContainerId;
        }
    }
}
