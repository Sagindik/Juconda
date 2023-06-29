using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;
using Juconda.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Juconda.ViewModels.Account
{
    public class PersonalCabinetViewModel : IMapFrom<User>
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; } 

        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; set; } 

        /// <summary>
        /// Отчество
        /// </summary>
        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, PersonalCabinetViewModel>().ReverseMap();
        }
    }
}

