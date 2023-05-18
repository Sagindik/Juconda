using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class Product : CodeNameEntityBase
    {
        public string? Descsription { get; set; }

        public string? ShortDescription { get; set; }

        public decimal? Price { get; set; }

        public int Count { get; set; }

        public string? Image { get; set; }

        public string? Measure { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Страна производства
        /// </summary>
        public int? CountryOfProductionId { get; set; }
        public virtual CountryOfProduction? CountryOfProduction { get; set; }

        public virtual List<CartItem> CartItems { get; set; } = new();
    }
}
