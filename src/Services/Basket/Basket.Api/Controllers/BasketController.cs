using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly ICartRepository _cartRepository;
        private readonly ILogger<BasketController> _logger;
        private readonly DiscountGrpcServices _discountGrpcServices;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(ICartRepository cartRepository, DiscountGrpcServices discountGrpcServices, ILogger<BasketController> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _cartRepository = cartRepository;
             _discountGrpcServices = discountGrpcServices;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        [HttpGet("{userName}", Name ="GetCart")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetCart(string userName) {

            return Ok(await _cartRepository.GetCart(userName) ?? new ShoppingCart(userName) );

        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart cart)
        {
            
            foreach (var product in cart.items) {

              var coupon = await _discountGrpcServices.GetDiscount(product.ProductName);
                product.Price -= coupon.Amount;
            
            }

            return Ok(await _cartRepository.UpdateCart(cart));
        }


        [HttpDelete("{userName}",Name ="DeleteCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName) {
            
           await _cartRepository.DeleteCart(userName);

            return Ok();
        
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {

            var cart = await _cartRepository.GetCart(basketCheckout.UserName);
            if (cart is null)
            {
                _logger.LogError("CheckOut : Basket not found for user {userName}", basketCheckout.UserName);
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = cart.TotalPriceCal;
            await _publishEndpoint.Publish(eventMessage);
             


            await _cartRepository.DeleteCart(basketCheckout.UserName);

            return Ok();
        }
    }
}
