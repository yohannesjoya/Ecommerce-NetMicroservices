using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {

        public static IHost MigrateDb<TContext>(
            this IHost host, 
            Action<TContext ,IServiceProvider> seeder,
            int? retry=0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope()) {
           
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();
                var config = services.GetRequiredService<IConfiguration>();


                try {

                    logger.LogInformation($"Migrating database assiated with context named ${typeof(TContext).Name}");

                    InvokeSeeder(seeder,context,services);

                    logger.LogInformation($"FINISHED Migrating database assiated with context named ${typeof(TContext).Name}");


                }
                catch (SqlException ex) {

                    logger.LogError(ex, "An error occured while migrating the database of DiscountDb");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDb<TContext>(host,seeder, retryForAvailability);
                    }
                
                }

            }


                return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context,services);
        }
    }
}
