using Request.Aggregator.Moldes;

namespace Request.Aggregator.Services.Contracts
{
    public interface IOrderService
    {

        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
