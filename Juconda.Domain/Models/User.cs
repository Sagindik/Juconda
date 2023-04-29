using Microsoft.AspNetCore.Identity;

namespace Juconda.Domain.Models
{
    public class User : IdentityUser<long>
    {
        public User() : base()
        {
            //указываем флаг по умолчанию для возможности блокировки
            LockoutEnabled = true;
        }

        /// <summary>
        /// Дата последнего входа в систему
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        public virtual Guid? Uid { get; set; }

        /// <summary>
        /// Создание
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        /// <summary>
        /// Обновление
        /// </summary>
        public virtual DateTime? UpdateDate { get; set; }
    }
}


