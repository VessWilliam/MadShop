namespace Basket.API.Data.IService;

public interface IBasketRespository
{
    Task<ShopCart> GetBasketAsync(string UserName, CancellationToken cancellationToken = default);

    Task<ShopCart> StoreBasket(ShopCart basket, CancellationToken cancellationToken = default);

    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
