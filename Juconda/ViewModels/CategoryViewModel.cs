using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? FullDescsription { get; set; }
        public string? Description { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        public int? ParentId { get; set; }
        public CategoryViewModel? Parent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
