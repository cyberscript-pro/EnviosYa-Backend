using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.RestAPI.Data.Products;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/products")
            .WithTags("Products")
            .WithOpenApi();

        productGroup.MapPost("/", async ([FromBody] CreateProductDto request, [FromServices] ICommandHandler<CreateProductCommand, CreateProductResponseDto> handler) =>
        {
            var command = request.ToCommand();
            var result = await handler.Handle( command );

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

    }
}