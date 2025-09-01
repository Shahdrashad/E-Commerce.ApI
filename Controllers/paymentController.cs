using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.Services;
using E_Commerce.ApI.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ApI.Controllers
{

    //[Authorize]
    //public class paymentController : APIBaseController
    //{
    //    private readonly IPaymentService _paymentService;
    //    private readonly IMapper _mapper;

    //    public paymentController(IPaymentService paymentService,IMapper mapper)
    //    {
    //        _paymentService = paymentService;
    //        _mapper = mapper;
    //    }
    //    //create Or Update Endpoint
    //    [HttpPost]
    //    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentintent (string basketId)
    //    {
    //        var CustomerBasket =await _paymentService.CreateOrUpdatePaymentIntent(basketId);
    //        if (CustomerBasket is null) return BadRequest();
    //        var MappedBaket = _mapper.Map<CustomerBasket, CustomerBasketDto>(CustomerBasket);
    //        return Ok(MappedBaket);
    //    }
    //}
}
