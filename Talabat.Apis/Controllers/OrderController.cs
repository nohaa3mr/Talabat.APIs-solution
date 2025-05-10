using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
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

        public OrderController(IMapper mapper,IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status400BadRequest)]

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDTO , OrderAddress>(orderDto.ShippingAddress);
            var order =await _orderService.CreateOrderAsync(BuyerEmail , orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);

            if (order is null)
            return BadRequest(new ErrorApiResponse(400));
            return Ok(order);
        }

        [ProducesResponseType(typeof(IReadOnlyList<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderUserByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderForUserAsync(email);
            if (order is null)
                return BadRequest(new ErrorApiResponse(400, "No orders found."));
            var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(order);
        }
        [HttpGet("{OrderId}")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<OrderToReturnDto>> GetUserOrderById(int OrderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order =await _orderService.GetOrderByIdAsync(OrderId, email);
            
            if (order is null) return BadRequest(new ErrorApiResponse(400));
            var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(mappedOrder);
        }
    }
}
