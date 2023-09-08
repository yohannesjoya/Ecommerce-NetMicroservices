using AutoMapper;
using Discount.Grpc.Dtos;
using Discount.Grpc.Protos;
using Discount.Grpc.repositories;
using Grpc.Core;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon is null) throw new RpcException(new Status(statusCode:StatusCode.NotFound, $"There is no Discount for {request.ProductName}"));

            var res = _mapper.Map<CouponModel>(coupon);

            _logger.LogInformation($"Discount Coupon for {request.ProductName} is found, Product Name = {coupon.ProductName},\n Product Description = {coupon.Description},\n Amount = {coupon.Amount}");

            return res;

        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponToBeCreated = _mapper.Map<CreateCouponDto>(request.Coupon);
      
            bool success = await _repository.CreateDiscount(couponToBeCreated);

            var res = _mapper.Map<CouponModel>(couponToBeCreated);

            _logger.LogInformation($"Discount Coupon for {request.Coupon.ProductName} is Created = {success}.");

            return res;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponToBeEdited = _mapper.Map<UpdateCouponDto>(request.Coupon);

            bool success = await _repository.UpdateDiscount(couponToBeEdited);

            var res = _mapper.Map<CouponModel>(couponToBeEdited);

            _logger.LogInformation($"Discount Coupon for {request.Coupon.ProductName} is Edited = {success}");

            return res;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {

            var success = await _repository.DeleteDiscount(request.ProductName);

            _logger.LogInformation($"Deletion of Discount Coupon for {request.ProductName} is {success}");

            return new DeleteDiscountResponse { Success = success};

        }
    }
}
