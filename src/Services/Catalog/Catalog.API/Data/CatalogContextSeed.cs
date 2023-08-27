using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {



        public static void SeedData(IMongoCollection<Product> prdTable) {
        
            bool existProduct = prdTable.Find(p => true).Any();

            if (!existProduct) {

                prdTable.InsertManyAsync(GetPreparedProducts());
            
            }
        
        }

        private static IEnumerable<Product> GetPreparedProducts()
        {
            return new List<Product>() {

            new Product
                {
                    Id = "507f1f77bcf86cd799439011",
                    Name = "Samsung S21",
                    Category = "Electronics",
                    Summary = "A high-end smartphone with the latest features.",
                    Description = "This smartphone has a large, high-resolution display, a powerful processor, and multiple cameras for stunning photos and videos. It also has a long-lasting battery and supports fast charging.",
                    ImageFile = "smartphone.jpg",
                    Price = 799.99M
                },

            new Product
                {
                    Id = "507f1f77bcf86cd799439012",
                    Name = "Hp Laptop",
                    Category = "Electronics",
                    Summary = "A lightweight and portable laptop for work and entertainment.",
                    Description = "This laptop has a fast processor, plenty of storage, and a high-resolution display. It's perfect for work, browsing the web, and streaming your favorite shows and movies.",
                    ImageFile = "laptop.jpg",
                    Price = 999.99M
                },

            new Product
                {
                    Id = "507f1f77bcf86cd799439013",
                    Name = "Apple Watch",
                    Category = "Electronics",
                    Summary = "A stylish smartwatch with fitness tracking and notification features.",
                    Description = "This smartwatch can track your daily activity, monitor your heart rate, and display notifications from your phone. It also has a long battery life and is water-resistant.",
                    ImageFile = "smartwatch.jpg",
                    Price = 199.99M
                }



        };
        }
    }
}
