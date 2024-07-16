using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.API.Data;

public class CachedCartRepository
    (ICartRepository repository, IDistributedCache cache) 
    : ICartRepository
{
    public async Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default)
    {
        var cachedCart = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedCart))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedCart)!;

        var Cart = await repository.GetCart(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(Cart), cancellationToken);
        return Cart;
    }

    public async Task<ShoppingCart> StoreCart(ShoppingCart Cart, CancellationToken cancellationToken = default)
    {
        await repository.StoreCart(Cart, cancellationToken);

        await cache.SetStringAsync(Cart.UserName, JsonSerializer.Serialize(Cart), cancellationToken);

        return Cart;
    }

    public async Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteCart(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
