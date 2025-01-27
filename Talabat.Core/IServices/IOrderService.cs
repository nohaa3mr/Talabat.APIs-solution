using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string buyerEmail , string BasketId , int DeliveryMethodId , OrderAddress orderAddress);
        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        public Task<Order?> GetOrderByIdAsync(int orderId, string buyerEmail);

    }
}
