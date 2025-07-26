using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Cart.Queries.GetAll;
using EnviosYa.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class Cart
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        var cartGroup = app.MapGroup("/cart")
            .WithTags("Cart")
            .WithOpenApi();

        cartGroup.MapGet("/", async ([FromServices] IQueryHandler<GetAllCartQuery, List<GetAllCartResponseDto>> handler) =>
            {
                var result = await handler.Handle(new GetAllCartQuery());

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "FindAll a cart",
            Description = "find all cart in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Cart" } }
        }).RequireAuthorization(policy => policy.RequireRole(nameof(RolUser.Admin)));

    }
}