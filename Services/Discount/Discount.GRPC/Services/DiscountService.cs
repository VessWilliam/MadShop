using Discount.Grpc;
using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName.Equals(request.ProductName));

        if (coupon is null)
            coupon = new Coupons
            {
                ProductName = "No Discount Available",
                Amount = 0,
                Description = "No discount available"
            };


        logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);


        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}
