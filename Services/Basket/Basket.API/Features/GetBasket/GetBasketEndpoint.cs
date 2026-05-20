

namespace Basket.API.Features.GetBasket;

//public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShopCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
         {
             var result = await sender.Send(new GetBasketQuery(userName));

             var response = result.Adapt<GetBasketResponse>();

             return Results.Ok(response);
         })
         .WithName("GetBasketProductById")
         .Produces<GetBasketResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Basket Product By UserName")
         .WithDescription("Get Basket Product By UserName");
    }
}
