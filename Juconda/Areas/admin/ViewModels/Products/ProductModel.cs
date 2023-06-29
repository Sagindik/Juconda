using AutoMapper;
using Juconda.Areas.admin.ViewModels.Categories;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.Areas.admin.ViewModels.Products
{
    public class ProductModel : IMapFrom<Product>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public string? CountryOfProductionName { get; set; }

        public List<string> CategoryNames { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.CategoryProducts.Where(_ => _.Actual).Select(_ => _.Category.Name)))
                .ReverseMap();
               
        }
    }
}
