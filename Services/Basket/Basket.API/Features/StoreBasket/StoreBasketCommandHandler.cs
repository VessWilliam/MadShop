using Discount.Grpc;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketResult(string UserName);

public record StoreBasketCommand(ShopCart Cart) : ICommand<StoreBasketResult>;


public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart is required");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler(IBasketRespository respository,
    DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await respository.StoreBasket(command.Cart, cancellationToken);

        return new(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShopCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken);

            item.Price -= coupon.Amount;
        }
    }
}
