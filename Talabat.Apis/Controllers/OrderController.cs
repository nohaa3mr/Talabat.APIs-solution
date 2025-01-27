using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Apis.ErrorsHandler;
using Talabat.Apis.Helpers;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;
using Talabat.Core.IServices;
using Talabat.Repositories.Interfaces.Contract;

namespace Talabat.Apis.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(   IMapper mapper , IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status400BadRequest)]

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDTO , OrderAddress>(orderDto.ShippingAddress);
            var order =await _orderService.CreateOrderAsync(BuyerEmail , orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);

            if (order is null)
            return BadRequest(new ErrorApiResponse(400));
            return Ok(order);
        }
    }
}
