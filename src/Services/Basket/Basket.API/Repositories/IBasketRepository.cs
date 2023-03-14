using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> Get(string username);
        Task<ShoppingCart> Update(ShoppingCart basket);
        Task Delete(string username);
    }
}
