
using Domain.Entities.Basket;
using Domain.Exceptions;
using Shared.Basket.Dto;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {


        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
           var CustomerBasket=_mapper.Map<CustomerBasket>(basket);
          var IsCreatedOrUpdated= await  _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (IsCreatedOrUpdated is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now, Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string key)=> await _basketRepository.DeleteBasketAsync(key);
       

        public async Task<BasketDto> GetBasketAsync(string key)
        {
           var Basket=await _basketRepository.GetBasketAsync(key);

            if (Basket is not null)
                return _mapper.Map<BasketDto>(Basket);
            else
                throw new BasketNotFoundException(key);
        }
    }
}
