using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
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

        public BasketController(ICartRepository cartRepository, DiscountGrpcServices discountGrpcServices, ILogger<BasketController> logger)
        {
            _cartRepository = cartRepository;
             _discountGrpcServices = discountGrpcServices;
            _logger = logger;
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
            Console.WriteLine("============== Discount is Done on the cart");

            foreach (var product in cart.items)
            {

                Console.WriteLine($"after Discount {product.ProductName}, {product.Price}");


            }

            return Ok(await _cartRepository.UpdateCart(cart));
        }


        [HttpDelete("{userName}",Name ="DeleteCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName) {
            
           await _cartRepository.DeleteCart(userName);

            return Ok();
        
        }
    }
}
