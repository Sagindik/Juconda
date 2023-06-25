using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Juconda.Areas.admin.ViewModels.Categories
{
    public class CreateCategoryViewModel : IMapFrom<Category>
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        public int? ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryViewModel, Category>()
               .ForMember(dest => dest.Parent, opt => opt.Ignore());
        }
    }
}
