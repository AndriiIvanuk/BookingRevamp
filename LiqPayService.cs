using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BookingRevamp.Models;
using Microsoft.Extensions.Options;

namespace BookingRevamp.Services
{
    public class LiqPayService
    {
        private readonly LiqPaySettings _settings;

        public LiqPayService(IOptions<LiqPaySettings> options)
        {
            _settings = options.Value;
        }

        public (string Data, string Signature) CreatePayment(
            Booking booking,
            string resultUrl,
            string serverUrl)
        {
            var payment = new
            {
                version = 3,
                public_key = _settings.PublicKey,
                action = "pay",

                amount = booking.Amount,
                currency = booking.Currency,

                description = $"Бронювання №{booking.Id}",

                order_id = booking.LiqPayOrderId,

                result_url = resultUrl,

                server_url = serverUrl
            };

            var json = JsonSerializer.Serialize(payment);

            var data = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(json));

            var signature = CreateSignature(data);

            return (data, signature);
        }

        private string CreateSignature(string data)
        {
            var sign = _settings.PrivateKey + data + _settings.PrivateKey;

            using var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(
                Encoding.UTF8.GetBytes(sign));

            return Convert.ToBase64String(hash);
        }
    }
}
