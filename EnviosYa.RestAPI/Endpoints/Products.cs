using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;

namespace EnviosYa.RestAPI.Endpoints;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/products")
            .WithTags("Products")
            .WithOpenApi();

        productGroup.MapPost("/products", async (ICommandHandler<CreateProductCommand, string> handler) =>
        {
            var result = await handler.Handle(
                new CreateProductCommand(), default
            );

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

    }
}