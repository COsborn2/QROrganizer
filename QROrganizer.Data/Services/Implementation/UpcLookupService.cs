using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class UpcLookupService : IUpcLookupService
{
    private readonly IBarcodeSpiderHttpClient _barcodeSpiderHttpClient;
    private readonly AppDbContext _context;
    private readonly ILogger<UpcLookupService> _logger;

    public UpcLookupService(
        IBarcodeSpiderHttpClient barcodeSpiderHttpClient,
        AppDbContext context,
        ILogger<UpcLookupService> logger)
    {
        _barcodeSpiderHttpClient = barcodeSpiderHttpClient
                                   ?? throw new ArgumentNullException(nameof(barcodeSpiderHttpClient));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ICollection<ItemBarcodeInformation>> LookupUpcCode(string upcCode)
    {
        var barcodeInformation = _context.ItemBarcodeInformation
            .Where(x => x.UpcCode == upcCode);

        if (barcodeInformation.Count() > 1)
        {
            return barcodeInformation.ToList();
        }

        var barcodeSpiderBarcode = await _barcodeSpiderHttpClient.LookupUpcCode(upcCode);
        if (barcodeSpiderBarcode.ItemResponse.StatusCode > 299)
        {
            _logger.LogWarning($"Barcode spider failed to process UpcCode '{upcCode}'");
            throw new InvalidOperationException("Failed to process UPC Code");
        }

        var itemBarcode = new ItemBarcodeInformation
        {
            Title = barcodeSpiderBarcode.ItemAttributes.Title,
            UpcCode = barcodeSpiderBarcode.ItemAttributes.UpcCode,
            EanCode = barcodeSpiderBarcode.ItemAttributes.EanCode,
            ParentCategory = barcodeSpiderBarcode.ItemAttributes.ParentCategory,
            Category = barcodeSpiderBarcode.ItemAttributes.Category,
            Brand = barcodeSpiderBarcode.ItemAttributes.Brand,
            Model = barcodeSpiderBarcode.ItemAttributes.Model,
            MpnCode = barcodeSpiderBarcode.ItemAttributes.MpnCode,
            Manufacturer = barcodeSpiderBarcode.ItemAttributes.Manufacturer,
            Publisher = barcodeSpiderBarcode.ItemAttributes.Publisher,
            AsinCode = barcodeSpiderBarcode.ItemAttributes.AsinCode,
            Color = barcodeSpiderBarcode.ItemAttributes.Color,
            Size = barcodeSpiderBarcode.ItemAttributes.Size,
            Weight = barcodeSpiderBarcode.ItemAttributes.Weight,
            ImageLink = barcodeSpiderBarcode.ItemAttributes.ImageLink,
            IsAdult = barcodeSpiderBarcode.ItemAttributes.IsAdult,
            Description = barcodeSpiderBarcode.ItemAttributes.Description,
            LowestPrice = barcodeSpiderBarcode.ItemAttributes.LowestPrice,
            HighestPrice = barcodeSpiderBarcode.ItemAttributes.HighestPrice
        };
        _context.ItemBarcodeInformation.Add(itemBarcode);
        await _context.SaveChangesAsync();

        return new[] { itemBarcode };
    }
}
