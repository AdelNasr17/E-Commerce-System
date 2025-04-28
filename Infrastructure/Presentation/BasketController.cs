using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.Basket.Dto;


namespace Presentation
{
  
    public class BasketController(IServicesManager servicesManager): ApiBaseController
    {

        //Get Basket

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket=await servicesManager.basketService.GetBasketAsync(key);

            return Ok(Basket);

        }

        // Create Or Update Basket 
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket= await servicesManager.basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete Basket 
        [HttpDelete("{Key}")]
        public async Task<ActionResult<BasketDto>> DeleteBasket(string Key)
        {
            var Basket=await servicesManager.basketService.DeleteBasketAsync(Key);
            return Ok(Basket); 
        }

    }
}
