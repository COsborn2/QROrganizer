using System.Collections.Generic;
using Newtonsoft.Json;

namespace QROrganizer.Data.Services.Models;

public class BarcodeSpiderLookupResponse
{
    [JsonProperty("item_response")]
    public BarcodeSpiderItemResponse ItemResponse { get; set; }

    [JsonProperty("item_attributes")]
    public BarcodeSpiderItemAttributes ItemAttributes { get; set; }

    [JsonProperty("Stores")]
    public ICollection<BarcodeSpiderStores> Stores { get; set; }
}
