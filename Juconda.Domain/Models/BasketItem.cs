using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    /// <summary>
    /// Элемент корзины 
    /// </summary>
    public class BasketItem : BaseEntity
    {
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>
        /// Корзина
        /// </summary>
        public int? BasketId { get; set; }
        public virtual Basket? Basket { get; set; }
    }
}
