using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    public class DbConnectionWrapper : IDbConnectionWrapper
    {
        public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, CommandDefinition command)
        {
            return connection.ExecuteScalarAsync<T>(command);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, CommandDefinition command)
        {
            return connection.QueryAsync<T>(command);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, CommandDefinition command)
        {
            return connection.QueryFirstOrDefaultAsync<T>(command);
        }

        public Task<int> ExecuteAsync(IDbConnection connection, CommandDefinition command)
        {
            return connection.ExecuteAsync(command);
        }
    }
}
