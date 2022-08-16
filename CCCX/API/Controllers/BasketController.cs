using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
                private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) //gets basket/cart by basketID 
        {
            var basket = await _basketRepository.GetBasketAsync(id); //gets basket/cart ID in CustomerBasketDTO

            return Ok(basket ?? new CustomerBasket(id)); //if basket/cart is null return new CustomerBasket(id)
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket) // updates basket/cart
       /* {
            var customerBasket = await _basketRepository.GetBasketAsync(basket.Id); //gets basket/cart ID in CustomerBasketDTO
            if (customerBasket == null) return BadRequest("Basket not found"); //if basket/cart is null return bad request
            customerBasket.Items = new List<BasketItem>(); //new list of basket items
            foreach (var item in basket.Items) //for each item in basket/cart
            {
                var productItem = await _basketRepository.GetBasketItemAsync(item.Id); //gets basket/cart item ID in BasketItemDTO
                if (productItem == null) return BadRequest("Basket item not found"); //if basket/cart item is null return bad request
                productItem.Quantity = item.Quantity; //sets quantity of basket/cart item
                customerBasket.Items.Add(productItem); //adds basket/cart item to list of basket/cart items
            }
            await _basketRepository.UpdateBasketAsync(customerBasket); //updates basket/cart
            return Ok(customerBasket); //returns basket/cart
        } */
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket); //maps basket/cart to CustomerBasket

            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket); //updates basket/cart

            return Ok(updatedBasket); //returns basket/cart
        }

        [HttpDelete] //DELETE is a method that deletes data from the server
        public async Task DeleteBasketAsync(string id) //deletes basket/cart using basket Id
        {
            await _basketRepository.DeleteBasketAsync(id); //deletes basket/cart
        }
    }
}