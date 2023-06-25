using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class Product : NameEntityBase
    {
        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public decimal? OldPrice { get; set; }
        public decimal? Price { get; set; }

        public string? Articul { get; set; }

        public bool IsNew { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsBestseller { get; set; }

        public int Count { get; set; }

        public byte[]? Image { get; set; }

        public string? Measure { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        /// <summary>
        /// Страна производства
        /// </summary>
        public int? CountryOfProductionId { get; set; }
        public virtual Country? CountryOfProduction { get; set; }

        public virtual List<BasketItem> BasketItems { get; set; } = new();

        public virtual List<CategoryProduct> CategoryProducts { get; set; } = new();
    }
}
