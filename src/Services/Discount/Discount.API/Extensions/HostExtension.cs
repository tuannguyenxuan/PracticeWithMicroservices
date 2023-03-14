using Discount.API.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var connectionPool = services.GetRequiredService<IConnectionPool>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    ExecuteMigrations(connectionPool);

                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");
                }
            }

            return host;
        }

        private static void ExecuteMigrations(IConnectionPool connectionPool)
        {
            //using var connection = new NpgsqlConnection(connectionPool.BuildConnectionString());
            //connection.Open();

            //using var command = new NpgsqlCommand
            //{
            //    Connection = connection
            //};

            //command.CommandText = "DROP TABLE IF EXISTS Coupons";
            //command.ExecuteNonQuery();

            //command.CommandText = @"CREATE TABLE Coupons(Id SERIAL PRIMARY KEY, 
            //                                                    ProductName VARCHAR(24) NOT NULL,
            //                                                    Description TEXT,
            //                                                    Amount INT)";
            //command.ExecuteNonQuery();


            //command.CommandText = "INSERT INTO Coupons(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            //command.ExecuteNonQuery();

            //command.CommandText = "INSERT INTO Coupons(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            //command.ExecuteNonQuery();
        }
    }
}
