using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class OrderProduct : BaseEntity
    {
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public int Count { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal PriceTotal { get; set; }
    }
}
