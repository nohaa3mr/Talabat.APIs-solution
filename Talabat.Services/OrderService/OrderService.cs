using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Interfaces;
using Talabat.Core.IServices;
using Talabat.Core.Specifications;
using Talabat.Repositories.Interfaces.Contract;

namespace Talabat.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository , IUnitOfWork unitOfWork )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodId, OrderAddress orderAddress)
        {
            var basket =await _basketRepository.GetBasket(BasketId);
            var OrderItems = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product =await _unitOfWork.Repository<Product>().GetById(item.Id);
                    var ProductItemOrdered = new ProductItemOrdered(product.Id , product.Name , product.PictureURL);
                    var OrderItem = new OrderItem(ProductItemOrdered , item.Quantity , item.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            var SumTotal = OrderItems.Sum(item => item.Quantity * item.Price);

            var DeliveryMethod =await _unitOfWork.Repository<DeliveryMethod>().GetById(DeliveryMethodId);
            //now create the order 
            var Order = new Order(buyerEmail, orderAddress, OrderItems, DeliveryMethod, SumTotal);
            await _unitOfWork.Repository<Order>().AddAsync(Order);
            var Result =   await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null ;
            
                return Order;
            

        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var Spec = new OrderSpecifications(buyerEmail);
            var Order =await _unitOfWork.Repository<Order>().GetAllWithSpec(Spec);
            return Order;

        }

        public async Task<Order?> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail ,orderId);
            var OrderId = await _unitOfWork.Repository<Order>().GetProductByIdWithSpec(spec);

            return OrderId;

        }
    }
}
