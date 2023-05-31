using Microsoft.AspNetCore.Identity;

namespace Juconda.Domain.Models.Users
{
    public class User : IdentityUser<long>
    {
        public User() : base()
        {
            //указываем флаг по умолчанию для возможности блокировки
            LockoutEnabled = true;
        }

        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        /// <summary>
        /// Дата последнего входа в систему
        /// </summary>
        public DateTimeOffset? LastLoginDate { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        public Guid? Uid { get; set; }

        /// <summary>
        /// Создание
        /// </summary>
        public DateTimeOffset? CreateDate { get; set; }

        /// <summary>
        /// Обновление
        /// </summary>
        public DateTimeOffset? UpdateDate { get; set; }
    }
}


