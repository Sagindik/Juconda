using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models.Users
{
    /// <summary>
    /// Профиль пользователя (личная информация)
    /// </summary>
    public class UserProfile : BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        public string Address { get; set; }

        public int? CityId { get; set; }
        public virtual City? City { get; set; }
    }
}
