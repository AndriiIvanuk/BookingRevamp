using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }

        public string PropertyName { get; set; }
    }
}