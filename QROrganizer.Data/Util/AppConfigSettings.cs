namespace QROrganizer.Data.Util
{
    public class AppConfigSettings
    {
        public string SendGridApiKey { get; set; }
        public bool RestrictedEnvironment { get; set; }
        public string HCaptchaSecret { get; set; }
    }
}
