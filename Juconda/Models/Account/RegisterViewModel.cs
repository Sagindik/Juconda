using System.ComponentModel.DataAnnotations;

namespace Juconda.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }

		[Required(ErrorMessage = "Почтовый адрес обязателен для заполнения")]
		public string Email { get; set; }		

		public string? ReturnUrl { get; set; }
    }
}
