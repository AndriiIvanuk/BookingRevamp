using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введіть email")]
        [EmailAddress(ErrorMessage = "Некоректний email")]
        public string Email { get; set; }
    }
}
