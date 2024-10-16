using LinkDev.Talabat.Core.Domain.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrasturcture.Basket_Repository
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);

        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket customerBasket, TimeSpan timeToLive)
        {
            var serializedBasket = JsonSerializer.Serialize(customerBasket);
            var updated = await _database.StringSetAsync(customerBasket.Id, serializedBasket, timeToLive);

            if (!updated) return null;

            return customerBasket;
            
        }
        public Task<bool> DeleteAsync(string id)
            => _database.KeyDeleteAsync(id);
        

    }
}
