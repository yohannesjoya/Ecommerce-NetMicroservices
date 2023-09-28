using Request.Aggregator.Extensions;
using Request.Aggregator.Moldes;
using Request.Aggregator.Services.Contracts;

namespace Request.Aggregator.Services
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var res = await _httpClient.GetAsync($"/api/v1/Order/{userName}");
            return await res.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
