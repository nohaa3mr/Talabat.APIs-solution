using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ErrorsHandler;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;
namespace Talabat.Apis.Controllers
{
   
    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> genericRepository , IMapper mapper)
        {
           _genericRepository = genericRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
          var Spec =new ProductSpecifications();
          var products =  await _genericRepository.GetAllWithSpec(Spec);
            var mappedAllProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return Ok(mappedAllProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product =await _genericRepository.GetProductByIdWithSpec(spec);
            if (product is null) return NotFound(new ErrorApiResponse(404));
            
            var mappedProductById = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(mappedProductById);
        }
    }
}
