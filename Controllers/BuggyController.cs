using E_commerce.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ApI.Controllers
{
    
    public class BuggyController : APIBaseController
    {
        private readonly AppDbContext _dbContext;

        public BuggyController(AppDbContext dbContext  )
        {
           _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        
        public ActionResult GetNotFoundRequest()
        {

            var product = _dbContext.Products.Find(100);
            if (product is null)
                return NotFound();
            return Ok(product);

        }
        [HttpGet ("ServerError")]
        public ActionResult GetServerError()
        {
            var product= _dbContext.Products.Find(100);
            var productToReturn = product.ToString();//error
            return Ok(productToReturn);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetbadRequest()
        {
            return BadRequest();

        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }



        }
}
