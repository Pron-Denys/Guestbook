namespace Guestbook.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    public class RegistrationModel
    {
        [Display(Name="Логін")]
        [Required(ErrorMessage="Заповніть поле")]
        [Remote(action:"CheckLogin", controller: "Account", ErrorMessage="Такий email вже використовується")]
        public string? Login { get; set; }
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Name { get; set; }
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Surname { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Скільки вам років")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Довжина повина бути від 4 до 10 символів")]
        [Remote(action: "CheckPassword", controller: "Account", ErrorMessage= "Довжина повина бути від 4 до 10 символів")]
        public string? Password { get; set; }
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Довжина повина бути від 4 до 10 символів")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Required(ErrorMessage = "Заповніть поле")]
        [Compare("Password", ErrorMessage="Паролі не збігаються")]
        public string? PasswordConfirm { get; set; }
    }
}
