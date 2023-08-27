using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }


        // ------------- 1. GetCart -------------
        public async Task<ShoppingCart> GetCart(string username)
        {
            var cart = await _redisCache.GetStringAsync(username);

            if (String.IsNullOrEmpty(cart))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(cart);

        }

        // ------------- 2. UpdateCart -------------
        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName,JsonConvert.SerializeObject(cart));
             
            return await GetCart(cart.UserName);
        
        }

        // ------------- 3. DeleteCart -------------
        public async Task DeleteCart(string username)
        { 
            await _redisCache.RemoveAsync(username);
        }

    }
}
