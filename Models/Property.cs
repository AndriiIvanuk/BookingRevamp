using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class Property
    {
        [Required]
        public int Id { get; set; }

        public int? OwnerId { get; set; }

        public User? Owner { get; set; } = null!;

        public string PropertyType { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Object { get; set; }

        public int MaxGuests { get; set; }

        public string Description { get; set; }

        public string Rules { get; set; }

        public List<PropertyImage> Images { get; set; } = new();

        public bool AllowPets { get; set; }

        public bool AllowChildren { get; set; }

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public ICollection<PropertyAmenity> Amenities { get; set; } = new List<PropertyAmenity>();

        public decimal PricePerNight { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
