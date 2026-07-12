using BookingRevamp.Models;

namespace BookingRevamp.ViewModels
{
    public class PaymentViewModel
    {
        public PendingPayment PendingPayment { get; set; } = null!;

        public Property Property { get; set; } = null!;

        public string Data { get; set; } = string.Empty;

        public string Signature { get; set; } = string.Empty;
    }
}