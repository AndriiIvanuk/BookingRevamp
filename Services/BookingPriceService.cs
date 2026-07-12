using BookingRevamp.Models;

namespace BookingRevamp.Services
{
    public class BookingPriceService
    {
        private const decimal TaxPercent = 0.05m;

        public BookingPriceResult Calculate(
            decimal pricePerNight,
            DateTime checkIn,
            DateTime checkOut)
        {
            var nights = (checkOut - checkIn).Days;

            var totalPrice = nights * pricePerNight;

            var taxes = Math.Ceiling(totalPrice * TaxPercent);

            return new BookingPriceResult
            {
                Nights = nights,
                TotalPrice = totalPrice,
                Taxes = taxes,
                GrandTotal = totalPrice + taxes
            };
        }
    }
}