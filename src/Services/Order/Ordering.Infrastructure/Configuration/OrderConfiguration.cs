using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        ILogger<OrderConfiguration> _logger;

        public OrderConfiguration(ILogger<OrderConfiguration> logger)
        {
            _logger = logger;
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(

                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            );

            _logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderDbContext).Name);
        }
    }
}
