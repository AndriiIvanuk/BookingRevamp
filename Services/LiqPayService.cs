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
    PendingPayment pendingPayment,
    string resultUrl,
    string serverUrl)
        {
            var payment = new
            {
                version = 3,

                public_key = _settings.PublicKey,

                action = "pay",

                amount = pendingPayment.Amount,

                currency = pendingPayment.Currency,

                description = $"Бронювання помешкання",

                order_id = pendingPayment.LiqPayOrderId,

                result_url = resultUrl,

                server_url = serverUrl
            };

            var json = JsonSerializer.Serialize(payment);

            Console.WriteLine("===== LIQPAY JSON =====");
            Console.WriteLine(json);

            var data = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(json));

            Console.WriteLine("===== LIQPAY DATA =====");
            Console.WriteLine(data);

            var signature = CreateSignature(data);

            Console.WriteLine("===== LIQPAY SIGNATURE =====");
            Console.WriteLine(signature);

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
