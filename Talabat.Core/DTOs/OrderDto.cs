
using Talabat.Apis.Helpers;

namespace Talabat.Core.DTOs
{
    public class OrderDto
    {
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }

}
