﻿namespace Cart.API.Data;

public interface ICartRepository
{
    Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreCart(ShoppingCart Cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default);
}
