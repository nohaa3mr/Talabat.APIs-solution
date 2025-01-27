using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ErrorsHandler;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basket , IMapper mapper)
        {
            _basket = basket;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basket.GetBasket(id);
            if (basket is null) return new CustomerBasket(id);
            return Ok(basket);

        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO customerBasket)
        {
            var MappedBasket = mapper.Map<CustomerBasketDTO, CustomerBasket>(customerBasket);
            var basket = await _basket.UpdateBasket(MappedBasket);
            if (basket is null) return BadRequest(new ErrorApiResponse(400));
          else  return Ok(basket);

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var basket = await _basket.DeleteBasket(id);
            return Ok(basket);

        }

    }
}
