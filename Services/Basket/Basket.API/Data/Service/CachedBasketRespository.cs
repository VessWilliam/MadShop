using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data.Service;

public class CachedBasketRespository(IBasketRespository respository, IDistributedCache cache) : IBasketRespository
{
    public async Task<ShopCart> GetBasketAsync(string UserName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(UserName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShopCart>(cachedBasket) ?? new();

        var basket = await respository.GetBasketAsync(UserName, cancellationToken);
        await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShopCart> StoreBasket(ShopCart basket, CancellationToken cancellationToken = default)
    {
        await respository.StoreBasket(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;

    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await respository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
