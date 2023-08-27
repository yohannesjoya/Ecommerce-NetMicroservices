using Basket.Api.Entities;
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

        public BasketController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
