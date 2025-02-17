﻿namespace Cart.API.Cart.StoreCart;

public record StoreCartCommand(ShoppingCart Cart) : ICommand<StoreCartResult>;
public record StoreCartResult(string UserName);

public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreCartCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreCartCommandHandler
    (ICartRepository repository)
    : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        // await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreCart(command.Cart, cancellationToken);

        return new StoreCartResult(command.Cart.UserName);
    }

    //private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    //{
    //    // Communicate with Discount.Grpc and calculate lastest prices of products into sc
    //    foreach (var item in cart.Items)
    //    {
    //        var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
    //        item.Price -= coupon.Amount;
    //    }
    //}
}