using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Order;

namespace Talabat.Apis.Helpers
{
    public class OrderItemPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                return $"{configuration["ApiUrl"]}{source.ProductItemOrdered.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
