using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<OrderDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("OrderConnectionString")));

            return services;
        
        }
    }
}
