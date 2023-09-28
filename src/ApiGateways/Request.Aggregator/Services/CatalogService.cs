using Request.Aggregator.Extensions;
using Request.Aggregator.Moldes;
using Request.Aggregator.Services.Contracts;

namespace Request.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {


        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var res = await _httpClient.GetAsync("/api/v1/Catalog");
            return await res.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalogById(string id)
        {
            var res = await _httpClient.GetAsync($"/api/v1/Catalog/{id}");
            return await res.ReadContentAs<CatalogModel>();

        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
          var res = await _httpClient.GetAsync($"/api/v1/GetProductsByCatagory/{category}");
          return await res.ReadContentAs<List<CatalogModel>>();
        }
    }
}
