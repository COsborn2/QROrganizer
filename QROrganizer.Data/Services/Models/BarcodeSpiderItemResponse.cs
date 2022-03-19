using Newtonsoft.Json;

namespace QROrganizer.Data.Services.Models;

public class BarcodeSpiderItemResponse
{
    [JsonProperty("code")]
    public string StatusCode { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}
