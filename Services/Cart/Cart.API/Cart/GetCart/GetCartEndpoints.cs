﻿namespace Cart.API.Cart.GetCart;

//public record GetCartRequest(string UserName); 
public record GetCartResponse(ShoppingCart Cart);

public class GetCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Cart/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery(userName));

            var respose = result.Adapt<GetCartResponse>();

            return Results.Ok(respose);
        })
        .WithName("GetProductById")
        .Produces<GetCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
