using Juconda.Domain.DomainModel;
using Juconda.Domain.Models.Users;

namespace Juconda.Domain.Models
{
    public class Basket : BaseEntity
    {
        public long? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? KeyGuid { get; set; }

        public virtual List<BasketItem> BasketItems { get; set; } = new();  
    }
}
