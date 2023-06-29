using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.ViewModels.ShoppingCart
{
    public class BasketViewModel : IMapFrom<Basket>
    {
        public int? Id { get; set; }

        public List<BasketItemViewModel> BasketItems { get; set; } = new();

        public decimal? TotalPrice { get; set; }
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Basket, BasketViewModel>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.BasketItems.Where(_ => _.Product != null).Sum(_ => _.Product.Price * _.Count)))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.BasketItems.Count));
        }
    }
}
