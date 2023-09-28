using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Aggregator.Moldes;
using Request.Aggregator.Services.Contracts;
using System.Net;

namespace Request.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }


        [HttpGet("{userName}",Name ="GetShoppingReport")]
        [ProducesResponseType(typeof(ShoppingModel),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShoppingReport(string userName)
        {
            var basketRes = await _basketService.GetBasket(userName);
            

            foreach (var item in basketRes.Items) {

                var product = await _catalogService.GetCatalogById(item.ProductId);

                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
                item.Category = product.Category;
                item.ProductName = product.Name;
                //item.Price = product.Price
            }

            var orderRes = await _orderService.GetOrdersByUserName(userName);

            var finalReport =  new ShoppingModel { UserName = userName, BasketWithProducts = basketRes, orders = orderRes };

            return Ok(finalReport);

        }


    }
}
