using Discount.Grpc.Dtos;
using Discount.Grpc.Entities;

namespace Discount.Grpc.repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productname);
        Task<bool> CreateDiscount(CreateCouponDto coupon); 
        Task<bool> UpdateDiscount(UpdateCouponDto coupon);
        Task<bool> DeleteDiscount(string productname);
    }
}
