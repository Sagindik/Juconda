using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class CategoryProduct : BaseEntity
    {
        /// <summary>
        /// Категория
        /// </summary>
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
