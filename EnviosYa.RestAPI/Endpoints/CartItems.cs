using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.CartItem.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class CartItems
{
    public static void MapCartItemsEndpoints(this WebApplication app)
    {
        var itemsGroup = app.MapGroup("/cart-items")
            .WithTags("CartItems")
            .WithOpenApi();

        itemsGroup.MapGet("/", async ([FromServices] IQueryHandler<GetAllCartItemQuery, List<GetAllCartItemResponseDto>> handler) =>
        {
            var result = await handler.Handle(new GetAllCartItemQuery());
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
        })
        .WithOpenApi(operation => new (operation)
        {
            Summary = "FindAll a cart items",
            Description = "find all cart items in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "CartItemsa" } }
        });
    }
}