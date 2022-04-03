using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.Utilities;
using Microsoft.AspNetCore.Authorization;
using QROrganizer.Data.Policies;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Models;

public class Item
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UpcCode { get; set; }

    [ForeignKey(nameof(UpcCode))]
    public ItemBarcodeInformation ItemBarcodeInformation { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; }

    [InternalUse]
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public int? ContainerId { get; set; }

    [ForeignKey(nameof(ContainerId))]
    public Container Container { get; set; }

    [Coalesce]
    public async Task<ItemResult> StartSearchingForUpcCode(
        [Inject] IUpcLookupService upcLookupService,
        [Inject] IAuthorizationService authorizationService,
        ClaimsPrincipal cp)
    {
        var authorized =
            await authorizationService.RequireFeaturePermission<ICollection<ItemBarcodeInformation>>(
                cp,
                Feature.BARCODE_LOOKUP);
        if (authorized is not null)
        {
            return authorized;
        }

        if (string.IsNullOrWhiteSpace(UpcCode)) return new ItemResult("Invalid UPC code");

        if (ItemBarcodeInformation is not null)
        {
            return new ItemResult();
        }

#pragma warning disable CS4014 // Don't want to await this here
        upcLookupService.LookupUpcCode(UpcCode);
#pragma warning restore CS4014

        return new ItemResult();
    }

    [Coalesce]
    public class DefaultBehaviors : StandardBehaviors<Item, AppDbContext>
    {
        private readonly CrudContext<AppDbContext> _context;

        public DefaultBehaviors(CrudContext<AppDbContext> context) : base(context)
        {
            _context = context;
        }

        public override ItemResult BeforeSave(SaveKind kind, Item oldItem, Item item)
        {
            if (_context.User is null) return "Unauthorized";

            item.UserId = _context.User.GetUserId();
            return base.BeforeSave(kind, oldItem, item);
        }
    }

    [Coalesce]
    [DefaultDataSource]
    public class DefaultDataSource : StandardDataSource<Item, AppDbContext>
    {
        public DefaultDataSource(CrudContext<AppDbContext> context) : base(context)
        { }

        public override IQueryable<Item> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => User.GetUserId() == x.UserId);
        }
    }

    [Coalesce]
    public class ItemsInContainer : DefaultDataSource
    {
        public ItemsInContainer(CrudContext<AppDbContext> context) : base(context)
        { }

        [Coalesce]
        public int ContainerId { get; set; }

        public override IQueryable<Item> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => x.ContainerId == ContainerId);
        }
    }
}
