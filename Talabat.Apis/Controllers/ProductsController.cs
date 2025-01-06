using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ErrorsHandler;
using Talabat.Apis.Helpers;
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
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductType> _typerepo;

        public ProductsController(IGenericRepository<Product> genericRepository , IMapper mapper,
            IGenericRepository<ProductBrand> brandrepo , IGenericRepository<ProductType> typerepo)
        {
           _genericRepository = genericRepository;
            _mapper = mapper;
           _brandrepo = brandrepo;
            _typerepo = typerepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecParams Params)
        {
          var Spec = new ProductSpecifications(Params);
          var products =  await _genericRepository.GetAllWithSpec(Spec);
          var Count = await _genericRepository.GetProductsWithCountAsync(new ProductSpecWithCount(Params));
          var mappedAllProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
          return Ok(new Pagination<ProductToReturnDto> (Params.Pagesize , Params.PageIndex , Count ,mappedAllProducts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _genericRepository.GetProductByIdWithSpec(spec);
            if (product is null)
           return NotFound(new ErrorApiResponse(400));

            var mappedProductById = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(mappedProductById);
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await _typerepo.GetAllAsync();
            return Ok(Types);
        }


        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _brandrepo.GetAllAsync();
            return Ok(brands);
        }
    }
}
