using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDb<TContext>(this IHost host, int? retry = 0) {
            
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var config = services.GetRequiredService<IConfiguration>();

                try
                {
                    logger.LogInformation("Migrated database");
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));

                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(24) NOT NULL, Description TEXT, Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung S21', 'Samsung Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('OnePlus 9 Pro', 'OnePlus Discount', 100);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated database");

                } catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occured while migrating the database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDb<TContext>(host, retryForAvailability);
                    }   

                   
                }
            }

            return host;
            
        }
    }
}
