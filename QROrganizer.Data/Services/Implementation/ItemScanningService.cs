using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.Utilities;
using Microsoft.AspNetCore.Authorization;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class ItemScanningService : IItemScanningService
{
    private readonly AppDbContext _context;
    private readonly IUpcLookupService _upcLookupService;
    private readonly IAuthorizationService _authorizationService;

    public ItemScanningService(
        AppDbContext context,
        IUpcLookupService upcLookupService,
        IAuthorizationService authorizationService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _upcLookupService = upcLookupService ?? throw new ArgumentNullException(nameof(upcLookupService));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    public async Task<ItemResult<Item>> CreateItemForUpcCodeAndStartSearch(
        [Inject] ClaimsPrincipal claimsPrincipal,
        string upcCode,
        int containerId)
    {
        var requiredFeature = Feature.BARCODE_LOOKUP.ToString();
        var authorized = await _authorizationService.AuthorizeAsync(
            claimsPrincipal,
            requiredFeature);

        if (!authorized.Succeeded)
        {
            return new ItemResult<Item>($"Unauthorized. Required Subscription Feature '{requiredFeature}'");
        }

        var itemBarcodeInformation = new ItemBarcodeInformation
        {
            UpcCode = upcCode
        };
        var newItem = new Item
        {
            UpcCode = upcCode,
            Name = "Added by Scanner",
            Quantity = 1,
            UserId = claimsPrincipal.GetUserId(),
            ContainerId = containerId,
        };
        await _context.ItemBarcodeInformation.AddAsync(itemBarcodeInformation);
        await _context.Items.AddAsync(newItem);
        await _context.SaveChangesAsync();

        // Item already has a UPC Code found -- and it's not the one we just added
        if (newItem.ItemBarcodeInformation is not null
            && newItem.ItemBarcodeInformation.Id != itemBarcodeInformation.Id)
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
