using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Utilities;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class ItemScanningService : IItemScanningService
{
    private readonly AppDbContext _context;
    private readonly IUpcLookupService _upcLookupService;

    public ItemScanningService(AppDbContext context, IUpcLookupService upcLookupService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _upcLookupService = upcLookupService ?? throw new ArgumentNullException(nameof(upcLookupService));
    }

    public async Task<Item> CreateItemForUpcCodeAndStartSearch(
        [Inject] ClaimsPrincipal claimsPrincipal,
        string upcCode,
        int containerId)
    {
        var newItem = new Item
        {
            UpcCode = upcCode,
            Name = "Added by Scanner",
            Quantity = 1,
            UserId = claimsPrincipal.GetUserId(),
            ContainerId = containerId,
        };
        _context.Items.Add(newItem);
        await _context.SaveChangesAsync();

        // Item already has a UPC Code found
        if (newItem.ItemBarcodeInformation is not null)
        {
            newItem.Name = newItem.ItemBarcodeInformation.Title;
            await _context.SaveChangesAsync();
            return newItem;
        }

#pragma warning disable CS4014 // we want this to go on in the background and not block the request
        _upcLookupService.LookupUpcCode(upcCode);
#pragma warning restore CS4014

        return newItem;
    }
}
