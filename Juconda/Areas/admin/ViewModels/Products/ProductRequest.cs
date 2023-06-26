using Juconda.Framework;

namespace Juconda.Areas.admin.ViewModels.Products
{
    public class ProductRequest : FilterRequest
    {
        public string? NameContains { get; set; }
    }
}
