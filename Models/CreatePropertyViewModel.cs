using Microsoft.AspNetCore.Http;

namespace BookingRevamp.Models{
    public class CreatePropertyViewModel
    {
        public string PropertyType { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Object { get; set; }

        public int MaxGuests { get; set; }

        public bool AllowPets { get; set; }

        public bool AllowChildren { get; set; }

        public string Description { get; set; }

        public string Rules { get; set; }

        public List<Amenity> Amenities { get; set; } = new();

        public List<int> SelectedAmenities { get; set; } = new();

        public decimal PricePerNight { get; set; }

        public List<IFormFile> Images { get; set; } = new();
    }
}
