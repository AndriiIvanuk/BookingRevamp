namespace BookingRevamp.Models
{
    public class PendingPayment
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int UserId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Guests { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "UAH";

        public string PaymentMethod { get; set; } = string.Empty;

        public string LiqPayOrderId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
