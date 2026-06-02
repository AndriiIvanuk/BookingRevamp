using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Хмм... здається, бракує email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        public string Password { get; set; }
    }
}