using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ErrorsHandler;
using Talabat.Repositories.Data;

namespace Talabat.Apis.Controllers
{

    public class BuggyController : ApiBaseController
    {
        private readonly TalabatDbContext _dbContext;

        public BuggyController(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFound()
        {
            var products = _dbContext.Products.Find(100);
            if (products is null) return NotFound(new ErrorApiResponse(404));
            return Ok(products);
        }

        [HttpGet("ServerError")]
        public ActionResult GetInternalServerError()
        {
            var products = _dbContext.Products.Find(100);
            var productToReturn = products.ToString(); //will throw an exception
            return Ok(productToReturn);

        }

        [HttpGet("BadRequest")]
        public ActionResult getBadRequest()
        {
            return BadRequest();

        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult BadRequest(int id)
        {
            return Ok();

        }
    }
}
