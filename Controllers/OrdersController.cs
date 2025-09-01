using E_commerce.Core.OrderAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.ApI.DTOS;
using E_commerce.Repository.Migrations;
using E_commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using E_commerce.Core.Repository;

namespace E_Commerce.ApI.Controllers
{
  
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #region Create order
        [HttpPut]
        //[Authorize]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        
        public async Task <ActionResult<Order>> CreateOrder (OrderDto orderDto)
        {
            var BuyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShipingAdress);
            var Order = _orderService.CreateOrderAsync (BuyerEmail, orderDto.BasketId,orderDto.DeliverMethodId, MappedAddress);
            if (Order is null) return BadRequest();
            return Ok(Order);
             
        }
        #endregion
        #region Get Orders
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]

        [HttpGet]
        //[Authorize]
       public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var BuyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var Orders=await _orderService.GetOrderForSpecificUserAsync (BuyerEmail);
            if(Orders is null) return BadRequest();
            var MapOrder=_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(MapOrder);

        }


        #endregion
        #region Get Order By Od
        [HttpGet("{id}")]
        //[Authorize]
        public async Task <ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {

            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
            if (Orders is null) return BadRequest();
            var MapOrder=_mapper.Map<OrderToReturnDto>(Orders);
            return Ok(MapOrder);
        }
        #endregion
        #region Get DEliveryMethod
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var DeliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok (DeliveryMethod);
        }
        #endregion
    }
}
