﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repositories.Interfaces.Contract
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository( IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string BasketId)
        {
            return  await  _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasket(string? BasketId)
        {
           var Basket =  await _database.StringGetAsync(BasketId);
           return Basket.IsNull ? null :JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasket(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
           var customerbasketUpdated =  await _database.StringSetAsync(basket.Id ,JsonBasket , TimeSpan.FromDays(1));
            if (!customerbasketUpdated) return null;
            else
                return await GetBasket(basket.Id);
        }
    }
}
