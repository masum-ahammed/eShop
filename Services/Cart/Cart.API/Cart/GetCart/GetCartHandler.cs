namespace Cart.API.Cart.GetCart;

public record GetCartQuery(string UserName) : IQuery<GetCartResult>;
public record GetCartResult(ShoppingCart Cart);

public class GetCartQueryHandler(ICartRepository repository)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var Cart = await repository.GetCart(query.UserName);

        return new GetCartResult(Cart);
    }
}
