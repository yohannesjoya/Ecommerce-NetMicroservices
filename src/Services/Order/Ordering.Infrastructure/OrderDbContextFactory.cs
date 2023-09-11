using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
    


            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            var builder = new DbContextOptionsBuilder<OrderDbContext>();
            var connectionString = configuration.GetConnectionString("OrderConnectionString");

            builder.UseSqlServer(connectionString);

            return new OrderDbContext(builder.Options);
        }
    }
}




