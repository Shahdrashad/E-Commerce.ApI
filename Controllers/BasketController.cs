using System.Security.Cryptography;
using E_commerce.Core.Entities;
using E_commerce.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ApI.Controllers
{
    [ApiController]
    
    public class BasketController
        : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository )
        {
            _basketRepository = basketRepository;
        }
        //Get or Recreate 
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var Result=await _basketRepository.GetBasketAsync(BasketId);
            return Result is null ? new CustomerBasket() : Result;

        }
        //update or create
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreateOrUpdate=await _basketRepository.UpdateBasketAsync(basket);
            if (CreateOrUpdate is null) return BadRequest();
            return Ok(CreateOrUpdate);
        }
        //Delete
        [HttpDelete]
        public async Task<ActionResult< bool>> DeleteBasket(string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }

    }
}
