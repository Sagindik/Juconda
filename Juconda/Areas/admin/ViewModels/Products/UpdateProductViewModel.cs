using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Juconda.Areas.admin.ViewModels.Products
{
    public class UpdateProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public decimal? OldPrice { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public decimal Price { get; set; }

        public string? Articul { get; set; }

        public bool IsNew { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsBestseller { get; set; }

        public int Count { get; set; }

        public List<int>? CategoryIds { get; set; } = new();

        [Required(ErrorMessage = "Please select an image file.")]
        [DataType(DataType.Upload)]
        [Display(Name="Картинка")]
        public IFormFile? ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductViewModel, Product>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.CategoryProducts.Where(_ => _.Actual).Select(_ => _.CategoryId)));
        }
    }
}
