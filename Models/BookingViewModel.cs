using BookingRevamp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingRevamp.ViewModels
{
    public class BookingViewModel
    {
        [ValidateNever]
        public Property Property { get; set; } = null!;

        public int PropertyId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Guests { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool BookingForAnotherPerson { get; set; }

        public string? GuestFullName { get; set; }

        public string? GuestEmail { get; set; }

        public string? GuestPhoneNumber { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string? CardHolder { get; set; }

        public string? CardNumber { get; set; }

        public string? CardDate { get; set; }

        public string? CardCvv { get; set; }

        public int Nights { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Taxes { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
