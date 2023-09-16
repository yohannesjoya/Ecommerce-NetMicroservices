using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;





namespace Ordering.Persistence
{

    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {

            string basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Ordering.Api");
            Console.WriteLine("***********************************************");

            Console.WriteLine($"{basePath}");
            Console.WriteLine("***********************************************");


            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var builder = new DbContextOptionsBuilder<OrderDbContext>();
            var connectionString = configuration.GetConnectionString("OrderConnectionString");

            builder.UseSqlServer(connectionString);

            return new OrderDbContext(builder.Options);

        }
    }
}