using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class CartItem : BaseEntity
    {
        public string CartId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;

        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
