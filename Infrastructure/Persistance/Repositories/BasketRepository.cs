

using Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var JsonBasket=JsonSerializer.Serialize(basket);
           var IsCreatedOrUpdated= await _database.StringSetAsync(basket.Id, JsonBasket,timeToLive?? TimeSpan.FromDays(30));

            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id) => await _database.KeyDeleteAsync(id);




        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);

            if(Basket.IsNullOrEmpty)
                return null;
            else 
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
