using AutoMapper;
using Discount.Grpc.Dtos;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<CouponModel, Coupon>().ReverseMap();
            CreateMap<CouponModel,UpdateCouponDto>().ReverseMap();
            CreateMap<CouponModel,CreateCouponDto>().ReverseMap();
        }

    }
}
