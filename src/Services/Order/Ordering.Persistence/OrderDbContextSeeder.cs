using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Persistence
{
    public class OrderDbContextSeeder
    {

        public static async Task SeedAsyncData(OrderDbContext dbContext, ILogger<OrderDbContext> logger) {

            if (!dbContext.Orders.Any()) {
            
                dbContext.Orders.AddRangeAsync(PreconfiguredOrdersData());
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Finished Seed database associated with context {DbContextName}", typeof(OrderDbContext).Name);

            }
        
        }
        private static IEnumerable<Order> PreconfiguredOrdersData()
        {
            return new List<Order>
            {
                new Order() {

               UserName = "JohnDoe123",
               TotalPrice = 99.99m,

               FirstName = "John",
               LastName = "Doe",
               EmailAddress = "john.doe@example.com",
               AddressLine = "123 Main St",
               Country = "United States",
               State = "California",
               ZipCode = "90001",

               CardName = "John Doe",
               CardNumber = "4111111111111111", // This is a dummy Visa card number
               Expiration = "12/25",
               CVV = "123",
               PaymentMethod = 1,

               CreatedBy = "AdminUser",
               CreatedDate = DateTime.Now,
               LastModifiedBy = "AdminUser",
               LastModifiedDate = DateTime.Now

           }
            };
        }
    }
}
