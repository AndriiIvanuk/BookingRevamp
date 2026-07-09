using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public int PropertyId { get; set; }

        public Property Property { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
