using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }
    }
}