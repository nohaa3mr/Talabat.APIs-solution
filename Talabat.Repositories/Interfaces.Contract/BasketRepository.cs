using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public bool DeleteBasket(string BasketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> GetBasket(string? BasketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            throw new NotImplementedException();
        }
    }
}
