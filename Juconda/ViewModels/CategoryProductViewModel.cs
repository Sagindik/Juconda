using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.ViewModels
{
    public class CategoryProductViewModel : IMapFrom<CategoryProduct>
    {
        public int Id { get; set; }

        // <summary>
        /// Категория
        /// </summary>
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public int? ProductId { get; set; }
        public ProductViewModel? Product { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryProduct, CategoryProductViewModel>().ReverseMap();
        }
    }
}
