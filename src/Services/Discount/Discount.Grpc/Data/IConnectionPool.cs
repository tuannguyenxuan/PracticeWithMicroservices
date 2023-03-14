using System.Data;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    public interface IConnectionPool
    {
        Task<IDbConnection> OpenPostgresConnectionAsync();
        public string BuildConnectionString();
    }
}
