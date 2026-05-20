

namespace Basket.API.Data.Service;

public class BasketRespository(IDocumentSession session) : IBasketRespository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShopCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShopCart> GetBasketAsync(string UserName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShopCart>(UserName, cancellationToken);
        return basket ?? throw new BasketNotFoundException(UserName);
    }

    public async Task<ShopCart> StoreBasket(ShopCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }
}
