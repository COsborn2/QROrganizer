using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class UpcLookupService : IUpcLookupService
{
    private readonly IBarcodeSpiderHttpClient _barcodeSpiderHttpClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpcLookupService> _logger;

    public UpcLookupService(
        IBarcodeSpiderHttpClient barcodeSpiderHttpClient,
        IServiceProvider serviceProvider,
        ILogger<UpcLookupService> logger)
    {
        _barcodeSpiderHttpClient = barcodeSpiderHttpClient
                                   ?? throw new ArgumentNullException(nameof(barcodeSpiderHttpClient));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ICollection<ItemBarcodeInformation>> LookupUpcCode(string upcCode)
    {
        ICollection<ItemBarcodeInformation> barcodeInformation;
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            barcodeInformation = await context.ItemBarcodeInformation
                .Where(x => x.UpcCode == upcCode)
                .ToListAsync();
        }

        if (barcodeInformation.Any())
        {
            return barcodeInformation.ToList();
        }

        ItemBarcodeInformation itemBarcode;
        var barcodeSpiderBarcode = await _barcodeSpiderHttpClient.LookupUpcCode(upcCode);
        if (barcodeSpiderBarcode.ItemResponse.StatusCode > 299)
        {
            _logger.LogWarning($"Barcode spider failed to process UpcCode '{upcCode}'");
            itemBarcode = new ItemBarcodeInformation
            {
                UpcCode = upcCode,
                WasSuccessful = false
            };
        }
        else
        {
            itemBarcode = new ItemBarcodeInformation
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
                HighestPrice = barcodeSpiderBarcode.ItemAttributes.HighestPrice,
                WasSuccessful = barcodeSpiderBarcode.ItemResponse.StatusCode / 100 == 2
            };
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.ItemBarcodeInformation.Add(itemBarcode);
            await context.SaveChangesAsync();
        }

        return new[] { itemBarcode };
    }
}
