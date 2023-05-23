using AutoMapper;
using Juconda.Core.Mappings;
using Juconda.Domain.Models;

namespace Juconda.ViewModels
{
    public class BasketItemViewModel : IMapFrom<BasketItem>
    {
        public int Id { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public int? ProductId { get; set; }
        public ProductViewModel? Product { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BasketItem, BasketItemViewModel>().ReverseMap();
        }
    }
}
