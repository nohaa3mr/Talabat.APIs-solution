using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;

namespace Talabat.Apis.Controllers
{
   
    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _genericRepository;

        public ProductsController(IGenericRepository<Product> genericRepository)
        {
           _genericRepository = genericRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
          var Spec =new ProductSpecifications();
          var products =  await _genericRepository.GetAllWithSpec(Spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product =await _genericRepository.GetProductByIdWithSpec(spec);
            return Ok(product);
        }
    }
}
