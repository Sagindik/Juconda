using Juconda.Domain.DomainModel;

namespace Juconda.Domain.Models
{
    public class Category : NameEntityBase
    {
        public int? ParentId { get; set; }
        public virtual Category? Parent { get; set; }
    }
}
