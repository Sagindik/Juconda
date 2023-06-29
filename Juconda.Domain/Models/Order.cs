using Juconda.Domain.DomainModel;
using Juconda.Domain.Models.Users;

namespace Juconda.Domain.Models
{
    public class Order : BaseEntity
    {
        public virtual User? User { get; set; }

        public int? CityId { get; set; }
        public virtual City? City { get; set; }

        public string? CommentInOrder { get; set; }

        public bool IsPaid { get; set; }
        public decimal PriceDelivery { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }

        public decimal Sum { get; set; }
        public int Total { get; set; }
    }
}
