namespace BookingRevamp.Models
{
    public class Amenity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Category { get; set; }

        public string Size { get; set; }

        public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
    }
}
