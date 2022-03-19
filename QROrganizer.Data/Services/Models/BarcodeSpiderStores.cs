using Newtonsoft.Json;

namespace QROrganizer.Data.Services.Models;

public class BarcodeSpiderStores
{
    [JsonProperty("store_name")]
    public string StoreName { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("price")]
    public string Price { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("updated")]
    public string Updated { get; set; }
}
