using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class Order:BaseEntity
    {
        public Order(string buyerEmail, DateTimeOffset orderDate, OrderAddress shippingAddress, ICollection<OrderItem> orderItems,DeliveryMethod deliveryMethod, decimal subTotal  )
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }
        public Order()
        {
            
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } =DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal{ get; set; }
        public decimal GetTotal() => SubTotal + deliveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }

}
