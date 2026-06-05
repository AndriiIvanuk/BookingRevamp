using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string PropertyType { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Object { get; set; }

        public int MaxGuests { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Rules { get; set; }
        [Required]
        public List<PropertyImage> Images { get; set; }

        public bool AllowPets { get; set; }

        public bool AllowChildren { get; set; }

        public bool FastWifi { get; set; }

        public bool AirConditioner { get; set; }

        public bool Heating { get; set; }

        public bool TV { get; set; }

        public bool Generator { get; set; }

        public bool Elevator { get; set; }

        public bool Wardrobe { get; set; }

        public bool Bedclothes { get; set; }

        public bool Iron { get; set; }

        public bool Safe { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }
        [Required]
        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
