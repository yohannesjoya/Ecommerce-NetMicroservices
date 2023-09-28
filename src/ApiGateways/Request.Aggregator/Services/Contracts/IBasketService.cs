using Request.Aggregator.Moldes;

namespace Request.Aggregator.Services.Contracts
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
