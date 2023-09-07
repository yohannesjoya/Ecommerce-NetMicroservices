using Discount.API.Dtos;
using Discount.API.Entities;

namespace Discount.API.repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productname);
        Task<bool> CreateDiscount(CreateCouponDto coupon); 
        Task<bool> UpdateDiscount(UpdateCouponDto coupon);
        Task<bool> DeleteDiscount(string productname);
    }
}
