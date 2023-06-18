using System.ComponentModel.DataAnnotations;

namespace Juconda.Areas.admin.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
