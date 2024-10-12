using StackExchange.Redis;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var CreateOrUpdateBasket = await _database.StringSetAsync(Basket.Id , JsonSerializer.Serialize(Basket), TimeSpan.FromDays(30));
            if(CreateOrUpdateBasket is false) return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
