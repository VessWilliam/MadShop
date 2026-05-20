namespace Basket.API.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShopCart Cart);

public class GetBasketQueryHandler(IBasketRespository respository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await respository.GetBasketAsync(query.UserName);

        return new(basket);
    }
}

