using BookingRevamp.Models;

namespace BookingRevamp.ViewModels
{
    public class BookingResultViewModel
    {
        public bool Success { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public Booking Booking { get; set; } = null!;
    }
}
