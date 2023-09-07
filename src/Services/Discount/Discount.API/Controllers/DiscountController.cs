using Discount.API.Dtos;
using Discount.API.Entities;
using Discount.API.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        //  ------------------------- 1. GetDiscount -------------------------
        [HttpGet("{productname}", Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Get(string productname) {
             var coupon = await _repository.GetDiscount(productname);
            return Ok(coupon);
        
        }

        //  ------------------------- 2. CreateDiscount -------------------------
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Create([FromBody] CreateCouponDto coupon)
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productname = coupon.ProductName }, coupon);
        }

        //  ------------------------- 3. UpdateDiscount -------------------------
        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Update([FromBody] UpdateCouponDto coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        //  ------------------------- 4. DeleteDiscount -------------------------
        [HttpDelete("{productname}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Delete(string productname)
        {
            return Ok(await _repository.DeleteDiscount(productname));
        }






    }
}
