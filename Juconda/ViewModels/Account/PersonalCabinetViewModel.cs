using System.ComponentModel.DataAnnotations;

namespace Juconda.ViewModels.Account
{
    public class PersonalCabinetViewModel
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

