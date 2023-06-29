using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class Category : NameEntityBase
    {
        public byte[]? Image { get; set; }

        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        public int? ParentId { get; set; }
        public virtual Category? Parent { get; set; }

        public virtual List<CategoryProduct> CategoryProducts { get; set; } = new();
    }
}
