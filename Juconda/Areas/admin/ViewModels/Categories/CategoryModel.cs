using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.Areas.admin.ViewModels.Categories
{
    public class CategoryModel : IMapFrom<Category>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}
