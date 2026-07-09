using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        public Property Property { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        public int Guests { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "UAH";

        [Required]
        public string PaymentMethod { get; set; } = null!;

        public string BookingNumber { get; set; } = string.Empty;

        public BookingStatus Status { get; set; } = BookingStatus.PendingPayment;

        public string? CardLast4 { get; set; }

        public string? LiqPayOrderId { get; set; }

        public string? LiqPayTransactionId { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Nights => (CheckOut - CheckIn).Days;
    }
}