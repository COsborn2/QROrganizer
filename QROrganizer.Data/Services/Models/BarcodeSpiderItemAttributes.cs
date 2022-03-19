using Newtonsoft.Json;

namespace QROrganizer.Data.Services.Models;

public class BarcodeSpiderItemAttributes
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("upc")]
    public string UpcCode { get; set; }

    [JsonProperty("ean")]
    public string EanCode { get; set; }

    [JsonProperty("parent_category")]
    public string ParentCategory { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("brand")]
    public string Brand { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("mpn")]
    public string MpnCode { get; set; }

    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; }

    [JsonProperty("publisher")]
    public string Publisher { get; set; }

    [JsonProperty("asin")]
    public string AsinCode { get; set; }

    [JsonProperty("color")]
    public string Color { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("weight")]
    public string Weight { get; set; }

    [JsonProperty("image")]
    public string ImageLink { get; set; }

    [JsonProperty("is_adult")]
    public string IsAdult { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("lowest_price")]
    public string LowestPrice { get; set; }

    [JsonProperty("highest_price")]
    public string HighestPrice { get; set; }
}
