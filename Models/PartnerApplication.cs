using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public class PartnerApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string CardNumber { get; set; }

        public string? PassportPath { get; set; }

        public bool AcceptTerms { get; set; }
    }
}