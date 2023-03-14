using Discount.API.Configurations;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace Discount.API.Data
{
    public class ConnectionPool : IConnectionPool
    {
        private readonly PostgresDbConfig _postgresDbConfig;

        public ConnectionPool(IOptions<PostgresDbConfig> postgresDbConfig)
        {
            _postgresDbConfig = postgresDbConfig.Value;
        }

        public async Task<IDbConnection> OpenPostgresConnectionAsync()
        {
            var connection = new NpgsqlConnection(BuildConnectionString());

            await connection.OpenAsync();

            return connection;
        }

        public string BuildConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = _postgresDbConfig.Host,
                Port = _postgresDbConfig.Port,
                Database = _postgresDbConfig.Database,
                Username = _postgresDbConfig.User,
                Password = _postgresDbConfig.Password,
                //SslMode = SslMode.Prefer,
                //SearchPath = config.SearchPath,
                //MinPoolSize = config.MinPoolSize ?? 1,
                //KeepAlive = config.KeepAliveInSeconds ?? 35
            };

            return builder.ConnectionString;
        }
    }
}
