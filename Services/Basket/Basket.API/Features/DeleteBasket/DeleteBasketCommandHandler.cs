namespace Basket.API.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool Success);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator() =>
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
}

public class DeleteBasketCommandHandler(IBasketRespository respository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await respository.DeleteBasket(request.UserName, cancellationToken);

        return new(true);
    }
}
