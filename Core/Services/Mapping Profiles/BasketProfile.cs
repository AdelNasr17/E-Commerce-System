
using Domain.Entities.Basket;
using Shared.Basket.Dto;

namespace Services.Mapping_Profiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketDto>().ReverseMap();
        }
    }
}
