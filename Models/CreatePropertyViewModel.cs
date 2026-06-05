using Microsoft.AspNetCore.Http;

namespace BookingRevamp.Models{
    public class CreatePropertyViewModel
    {

        public string Title { get; set; }

        public string PropertyType { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Object { get; set; }

        public int MaxGuests { get; set; }

        public bool AllowPets { get; set; }

        public bool AllowChildren { get; set; }

        public string Description { get; set; }

        public string Rules { get; set; }

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

        public decimal PricePerNight { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
