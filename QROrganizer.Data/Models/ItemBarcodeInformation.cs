using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IntelliTect.Coalesce.DataAnnotations;

namespace QROrganizer.Data.Models;

[Create(Roles = Roles.Admin)]
[Delete(Roles = Roles.Admin)]
[Edit(Roles = Roles.Admin)]
[Read(Roles = Roles.Admin)]
public class ItemBarcodeInformation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Title")]
    public string Title { get; set; }

    [Column("UpcCode")]
    public string UpcCode { get; set; }

    public ICollection<Item> Items { get; set; }

    [Column("EanCode")]
    public string EanCode { get; set; }

    [Column("ParentCategory")]
    public string ParentCategory { get; set; }

    [Column("Category")]
    public string Category { get; set; }

    [Column("Brand")]
    public string Brand { get; set; }

    [Column("Model")]
    public string Model { get; set; }

    [Column("MpnCode")]
    public string MpnCode { get; set; }

    [Column("Manufacturer")]
    public string Manufacturer { get; set; }

    [Column("Publisher")]
    public string Publisher { get; set; }

    [Column("AsinCode")]
    public string AsinCode { get; set; }

    [Column("Color")]
    public string Color { get; set; }

    [Column("Size")]
    public string Size { get; set; }

    [Column("Weight")]
    public string Weight { get; set; }

    [Column("ImageLink")]
    public string ImageLink { get; set; }

    [Column("IsAdult")]
    public string IsAdult { get; set; }

    [Column("Description")]
    public string Description { get; set; }

    [Column("LowestPrice")]
    public string LowestPrice { get; set; }

    [Column("HighestPrice")]
    public string HighestPrice { get; set; }

    [Column("WasSuccessful")]
    public bool WasSuccessful { get; set; }
}
