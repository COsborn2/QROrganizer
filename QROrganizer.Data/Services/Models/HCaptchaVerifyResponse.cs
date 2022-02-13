using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QROrganizer.Data.Services.Models;

/// <summary>
/// https://docs.hcaptcha.com/
/// </summary>
public class HCaptchaVerifyResponse
{
    /// <summary>
    /// is the passcode valid, and does it meet security criteria you specified, e.g. sitekey?
    /// </summary>
    [JsonProperty("Success")]
    public bool Success { get; set; }

    /// <summary>
    /// // timestamp of the challenge (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
    /// </summary>
    [JsonProperty("challenge_ts")]
    public DateTimeOffset ChallengeTimestamp { get; set; }

    /// <summary>
    /// the hostname of the site where the challenge was solved
    /// </summary>
    [JsonProperty("hostname")]
    public string Hostname { get; set; }

    /// <summary>
    /// optional: whether the response will be credited
    /// </summary>
    [JsonProperty("credit")]
    public bool Credit { get; set; }

    /// <summary>
    /// optional: any error codes
    /// </summary>
    [JsonProperty("error-codes")]
    public ICollection<string> ErrorCodes { get; set; }
}
