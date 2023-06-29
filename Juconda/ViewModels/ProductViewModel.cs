using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        /// <summary>
        /// Количество на складе
        /// </summary>
        public int Count { get; set; }
        public byte[]? Image { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
