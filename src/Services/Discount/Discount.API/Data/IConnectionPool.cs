using System.Data;
using System.Threading.Tasks;

namespace Discount.API.Data
{
    public interface IConnectionPool
    {
        Task<IDbConnection> OpenPostgresConnectionAsync();
        public string BuildConnectionString();
    }
}
