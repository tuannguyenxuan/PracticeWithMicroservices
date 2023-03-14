using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    public interface IDbConnectionWrapper
    {
        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, CommandDefinition command);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, CommandDefinition command);
        Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, CommandDefinition command);
        Task<int> ExecuteAsync(IDbConnection connection, CommandDefinition command);
    }
}
