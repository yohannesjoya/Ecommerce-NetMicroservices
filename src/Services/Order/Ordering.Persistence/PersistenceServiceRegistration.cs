using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistence;
using Ordering.Persistence.Repositories;

namespace Ordering.Persistence
{
    public static class PersistenceServiceRegistration
    {

        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string x = configuration.GetConnectionString("OrderConnectionString");
            Console.WriteLine("===========================================================");
            Console.WriteLine(x);
            Console.WriteLine("===========================================================");

            services.AddDbContext<OrderDbContext>(options =>
               options.UseSqlServer(x));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();


            return services;
        }
    }
}
