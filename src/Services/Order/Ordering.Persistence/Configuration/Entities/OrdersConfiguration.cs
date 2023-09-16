using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Persistence.Configuration.Entities
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(

           new Order() {
               //Id = -1,

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
       );
        }
    }
}