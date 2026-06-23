using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models.ViewModels
{
    public class BecomePartnerViewModel
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        [Required(ErrorMessage = "Це поле тепер є обов'язковим для ідентифікації")]
        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле обов’язкове для заповнення")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Завантажте паспорт")]
        public IFormFile PassportFile { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Підтвердіть умови сервісу")]
        public bool AcceptTerms { get; set; }
    }
}