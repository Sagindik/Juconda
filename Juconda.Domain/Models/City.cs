using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class City : NameEntityBase
    {
        public decimal PriceDelivery { get; set; }
        public decimal MinPriceFreeDelivery { get; set; }

        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
