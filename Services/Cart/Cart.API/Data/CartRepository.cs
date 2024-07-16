namespace Cart.API.Data;

public class CartRepository(IDocumentSession session)
    : ICartRepository
{
    public async Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default)
    {
        var Cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        
        return Cart is null ? throw new CartNotFoundException(userName) : Cart;
    }

    public async Task<ShoppingCart> StoreCart(ShoppingCart Cart, CancellationToken cancellationToken = default)
    {
        session.Store(Cart);
        await session.SaveChangesAsync(cancellationToken);
        return Cart;
    }

    public async Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}
