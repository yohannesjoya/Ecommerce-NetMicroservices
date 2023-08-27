using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {


        private readonly ICatalogContext _dbContext;
        public ProductRepository(ICatalogContext dbcontext)
        {
            _dbContext = dbcontext;
            
        }

        //all products
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.Find(p => true).ToListAsync();
        }
        //get by id

        public async Task<Product> GetProduct(string id)
        {
            return await _dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        //get by catagory
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _dbContext.Products.Find(filter).ToListAsync();
        }
        //get by name 

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _dbContext.Products.Find(filter).ToListAsync();

        }
        // create product
        public async Task CreateProduct(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
        }
        // update product
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _dbContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
        // delete product

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _dbContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }




    }
}
