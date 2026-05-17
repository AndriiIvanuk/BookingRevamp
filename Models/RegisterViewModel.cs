using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Введи ім'я")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Як ми можемо до тебе звертатися?")]
    public string SurName { get; set; }

    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "Хмм... здається, бракує email")]
    [EmailAddress(ErrorMessage = "Невірний email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Залиш номер телефону")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Поле Обов'язкове для заповнення")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Ці поля не можна пропустити")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    public string ConfirmPassword { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Потрібно прийняти умови сервісу")]
    public bool Accept { get; set; }
}