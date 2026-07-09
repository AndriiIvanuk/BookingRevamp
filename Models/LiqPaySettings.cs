namespace BookingRevamp.Models
{
    public class LiqPaySettings
    {
        public string PublicKey { get; set; } = string.Empty;

        public string PrivateKey { get; set; } = string.Empty;

        public string ApiUrl { get; set; } = string.Empty;

        public string Currency { get; set; } = "UAH";

        public string Action { get; set; } = "pay";

        public string ResultUrl { get; set; } = string.Empty;

        public string ServerUrl { get; set; } = string.Empty;
    }
}
