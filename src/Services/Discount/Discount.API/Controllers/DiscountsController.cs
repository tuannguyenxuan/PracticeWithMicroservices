using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountsController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            var discount = await _discountRepository.GetDiscountAsync(productName);

            return Ok(discount);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var discount = await _discountRepository.Test();

            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount(Coupon coupon)
        {
            var discount = await _discountRepository.CreateDiscountAsync(coupon);

            return Ok(discount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(Coupon coupon)
        {
            var discount = await _discountRepository.UpdateDiscountAsync(coupon);

            return Ok(discount);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            var rs = await _discountRepository.DeleteDiscountAsync(productName);

            return Ok(rs);
        }
    }
}
