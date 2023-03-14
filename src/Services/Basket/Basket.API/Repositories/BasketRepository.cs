using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task Delete(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> Get(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> Update(ShoppingCart basket)
        {
            var json = JsonSerializer.Serialize(basket);
            await _redisCache.SetStringAsync(basket.Username, json);

            return await Get(basket.Username);
        }
    }
}
