using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket> GetBasket(string? BasketId);
        public Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        public bool DeleteBasket(string BasketId);

    }
}
