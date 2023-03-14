using Discount.Grpc.Entities;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscountAsync(string productName);
        Task<Coupon> CreateDiscountAsync(Coupon counpon);
        Task<Coupon> UpdateDiscountAsync(Coupon counpon);
        Task<bool> DeleteDiscountAsync(string productName);
        Task<string> Test();
    }
}
