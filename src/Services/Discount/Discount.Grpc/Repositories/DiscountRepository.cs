using Dapper;
using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConnectionPool _connectionPool;
        private readonly IDbConnectionWrapper _connectionWrapper;

        public DiscountRepository(IConnectionPool connectionPool, IDbConnectionWrapper connectionWrapper)
        {
            _connectionPool = connectionPool;
            _connectionWrapper = connectionWrapper;
        }

        public async Task<Coupon> CreateDiscountAsync(Coupon coupon)
        {
            string query = @"
INSERT INTO Coupons 
    (ProductName, Description, Amount)
VALUES
    (:ProductName, :Description, :Amount)
RETURNING id;";

            var parameters = new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };

            var command = new CommandDefinition(query, parameters);
            using var conn = await _connectionPool.OpenPostgresConnectionAsync();
            var affected = await _connectionWrapper.ExecuteScalarAsync<int>(conn, command);

            if (affected == 0)
                return null;

            return await GetDiscountAsync(coupon.ProductName);
        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            string query = @"
DELETE FROM Coupons 
WHERE ProductName = :ProductName";

            var parameters = new
            {
                ProductName = productName,
            };

            var command = new CommandDefinition(query, parameters);
            using var conn = await _connectionPool.OpenPostgresConnectionAsync();
            var affected = await _connectionWrapper.ExecuteAsync(conn, command);

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {

            string query = @"
SELECT 
    * 
FROM
    Coupons
WHERE
    ProductName = :ProductName";

            var parameters = new
            {
                ProductName = productName,
            };

            var command = new CommandDefinition(query, parameters);
            using var conn = await _connectionPool.OpenPostgresConnectionAsync();
            var coupon = await _connectionWrapper.QueryFirstOrDefaultAsync<Coupon>(conn, command);

            return coupon;
        }

        public Task<string> Test()
        {
            return Task.FromResult(_connectionPool.BuildConnectionString());
        }

        public async Task<Coupon> UpdateDiscountAsync(Coupon coupon)
        {
            string query = @"
UPDATE Coupons 
SET
    ProductName = :ProductName,
    Description = :Description,
    Amount = :Amount,
WHERE Id = :Id";

            var parameters = new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                Id = coupon.Id
            };

            var command = new CommandDefinition(query, parameters);
            using var conn = await _connectionPool.OpenPostgresConnectionAsync();
            var rs = await _connectionWrapper.ExecuteScalarAsync<int>(conn, command);

            return await GetDiscountAsync(coupon.ProductName);
        }
    }
}
