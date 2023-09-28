using Request.Aggregator.Moldes;

namespace Request.Aggregator.Services.Contracts
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();

        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);

        Task<CatalogModel> GetCatalogById(string id);

    }
}
