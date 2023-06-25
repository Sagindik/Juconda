using Juconda.Framework;

namespace Juconda.Areas.admin.ViewModels.Categories
{
    public class CategoryRequest : FilterRequest
    {
        public string? NameContains { get; set; }
    }
}
