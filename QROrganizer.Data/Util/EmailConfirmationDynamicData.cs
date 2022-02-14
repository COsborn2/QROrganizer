using Newtonsoft.Json;

namespace QROrganizer.Data.Util;

public class EmailConfirmationDynamicData
{
    [JsonProperty("url")]
    public string ConfirmationUrl { get; set; }
}
