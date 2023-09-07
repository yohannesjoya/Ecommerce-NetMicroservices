using Discount.API.Entities;

namespace Discount.API.repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productname);
        Task<bool> CreateDiscount(Coupon coupon); 
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productname);
    }
}
