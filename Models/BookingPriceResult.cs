namespace BookingRevamp.Models
{
    public class BookingPriceResult
    {
        public int Nights { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Taxes { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
