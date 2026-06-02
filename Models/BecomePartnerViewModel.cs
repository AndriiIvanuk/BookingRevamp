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

        [Required(ErrorMessage = "Введи номер рахунку")]
        [MinLength(19, ErrorMessage = "Номер картки повинен містити 16 цифр")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Введи дані рахунку")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Формат повинен бути MM/YY")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "Введи дані рахунку")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV повинен містити 3 цифри")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Завантажте паспорт")]
        public IFormFile PassportFile { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Підтвердіть умови сервісу")]
        public bool AcceptTerms { get; set; }
    }
}