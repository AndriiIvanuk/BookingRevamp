using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        public string? Patronymic { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string? Password {  get; set; }

        public string Role { get; set; } = "User";

        public List<PartnerApplication> PartnerApplications { get; set; } = new();

        public ICollection<Property> Properties { get; set; } = new List<Property>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
