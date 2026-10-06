namespace Guestbook.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    public class LoginModel
    {
        [Display(Name = "Логін")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Login {  get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Пароль")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Довжина повина бути від 4 до 10 символів")]
        [Remote(action: "CheckPassword", controller: "Account", ErrorMessage= "Довжина повина бути від 4 до 10 символів")]
        public string? Password { get; set; }
    }
}
