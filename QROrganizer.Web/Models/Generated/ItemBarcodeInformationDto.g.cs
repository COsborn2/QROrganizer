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
    public partial class ItemBarcodeInformationDtoGen : GeneratedDto<QROrganizer.Data.Models.ItemBarcodeInformation>
    {
        public ItemBarcodeInformationDtoGen() { }

        private int? _Id;
        private string _Title;
        private string _UpcCode;
        private System.Collections.Generic.ICollection<QROrganizer.Web.Models.ItemDtoGen> _Items;
        private string _EanCode;
        private string _ParentCategory;
        private string _Category;
        private string _Brand;
        private string _Model;
        private string _MpnCode;
        private string _Manufacturer;
        private string _Publisher;
        private string _AsinCode;
        private string _Color;
        private string _Size;
        private string _Weight;
        private string _ImageLink;
        private string _IsAdult;
        private string _Description;
        private string _LowestPrice;
        private string _HighestPrice;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string Title
        {
            get => _Title;
            set { _Title = value; Changed(nameof(Title)); }
        }
        public string UpcCode
        {
            get => _UpcCode;
            set { _UpcCode = value; Changed(nameof(UpcCode)); }
        }
        public System.Collections.Generic.ICollection<QROrganizer.Web.Models.ItemDtoGen> Items
        {
            get => _Items;
            set { _Items = value; Changed(nameof(Items)); }
        }
        public string EanCode
        {
            get => _EanCode;
            set { _EanCode = value; Changed(nameof(EanCode)); }
        }
        public string ParentCategory
        {
            get => _ParentCategory;
            set { _ParentCategory = value; Changed(nameof(ParentCategory)); }
        }
        public string Category
        {
            get => _Category;
            set { _Category = value; Changed(nameof(Category)); }
        }
        public string Brand
        {
            get => _Brand;
            set { _Brand = value; Changed(nameof(Brand)); }
        }
        public string Model
        {
            get => _Model;
            set { _Model = value; Changed(nameof(Model)); }
        }
        public string MpnCode
        {
            get => _MpnCode;
            set { _MpnCode = value; Changed(nameof(MpnCode)); }
        }
        public string Manufacturer
        {
            get => _Manufacturer;
            set { _Manufacturer = value; Changed(nameof(Manufacturer)); }
        }
        public string Publisher
        {
            get => _Publisher;
            set { _Publisher = value; Changed(nameof(Publisher)); }
        }
        public string AsinCode
        {
            get => _AsinCode;
            set { _AsinCode = value; Changed(nameof(AsinCode)); }
        }
        public string Color
        {
            get => _Color;
            set { _Color = value; Changed(nameof(Color)); }
        }
        public string Size
        {
            get => _Size;
            set { _Size = value; Changed(nameof(Size)); }
        }
        public string Weight
        {
            get => _Weight;
            set { _Weight = value; Changed(nameof(Weight)); }
        }
        public string ImageLink
        {
            get => _ImageLink;
            set { _ImageLink = value; Changed(nameof(ImageLink)); }
        }
        public string IsAdult
        {
            get => _IsAdult;
            set { _IsAdult = value; Changed(nameof(IsAdult)); }
        }
        public string Description
        {
            get => _Description;
            set { _Description = value; Changed(nameof(Description)); }
        }
        public string LowestPrice
        {
            get => _LowestPrice;
            set { _LowestPrice = value; Changed(nameof(LowestPrice)); }
        }
        public string HighestPrice
        {
            get => _HighestPrice;
            set { _HighestPrice = value; Changed(nameof(HighestPrice)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.ItemBarcodeInformation obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.Title = obj.Title;
            this.UpcCode = obj.UpcCode;
            this.EanCode = obj.EanCode;
            this.ParentCategory = obj.ParentCategory;
            this.Category = obj.Category;
            this.Brand = obj.Brand;
            this.Model = obj.Model;
            this.MpnCode = obj.MpnCode;
            this.Manufacturer = obj.Manufacturer;
            this.Publisher = obj.Publisher;
            this.AsinCode = obj.AsinCode;
            this.Color = obj.Color;
            this.Size = obj.Size;
            this.Weight = obj.Weight;
            this.ImageLink = obj.ImageLink;
            this.IsAdult = obj.IsAdult;
            this.Description = obj.Description;
            this.LowestPrice = obj.LowestPrice;
            this.HighestPrice = obj.HighestPrice;
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
        public override void MapTo(QROrganizer.Data.Models.ItemBarcodeInformation entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(Title))) entity.Title = Title;
            if (ShouldMapTo(nameof(UpcCode))) entity.UpcCode = UpcCode;
            if (ShouldMapTo(nameof(EanCode))) entity.EanCode = EanCode;
            if (ShouldMapTo(nameof(ParentCategory))) entity.ParentCategory = ParentCategory;
            if (ShouldMapTo(nameof(Category))) entity.Category = Category;
            if (ShouldMapTo(nameof(Brand))) entity.Brand = Brand;
            if (ShouldMapTo(nameof(Model))) entity.Model = Model;
            if (ShouldMapTo(nameof(MpnCode))) entity.MpnCode = MpnCode;
            if (ShouldMapTo(nameof(Manufacturer))) entity.Manufacturer = Manufacturer;
            if (ShouldMapTo(nameof(Publisher))) entity.Publisher = Publisher;
            if (ShouldMapTo(nameof(AsinCode))) entity.AsinCode = AsinCode;
            if (ShouldMapTo(nameof(Color))) entity.Color = Color;
            if (ShouldMapTo(nameof(Size))) entity.Size = Size;
            if (ShouldMapTo(nameof(Weight))) entity.Weight = Weight;
            if (ShouldMapTo(nameof(ImageLink))) entity.ImageLink = ImageLink;
            if (ShouldMapTo(nameof(IsAdult))) entity.IsAdult = IsAdult;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
            if (ShouldMapTo(nameof(LowestPrice))) entity.LowestPrice = LowestPrice;
            if (ShouldMapTo(nameof(HighestPrice))) entity.HighestPrice = HighestPrice;
        }
    }
}
