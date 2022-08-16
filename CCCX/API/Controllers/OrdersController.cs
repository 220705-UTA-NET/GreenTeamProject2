using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) //creates order
       
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal(); //retrieves email from principal

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress); //maps ship to address to address

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address); //creates order

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order")); //if order is null return bad request

            return Ok(order); //returns order if successful
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser() //gets orders for user
        {
            var email = User.RetrieveEmailFromPrincipal(); //retrieves email from principal

            var orders = await _orderService.GetOrdersForUserAsync(email); //gets orders for user using email to track

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders)); //returns orders if successful
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id) //gets order by id for user
        {
            var email = User.RetrieveEmailFromPrincipal(); //retrieves email from principal

            var order = await _orderService.GetOrderByIdAsync(id, email); //gets order by id for user using email to track

            if (order == null) return NotFound(new ApiResponse(404)); //if order is null return not found

            return _mapper.Map<OrderToReturnDto>(order); //returns order if successful
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods() //delivery method
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync()); //returns delivery methods if successful
        }
    }
}