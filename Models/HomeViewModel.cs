namespace BookingRevamp.Models
{
    public class HomeViewModel
    {
        public List<Property> Houses { get; set; } = new();

        public List<Property> Hotels { get; set; } = new();

        public List<Property> Apartments { get; set; } = new();

        public List<Property> Villas { get; set; } = new();
    }
}
