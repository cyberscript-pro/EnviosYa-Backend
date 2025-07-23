using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Domain.Common;
using EnviosYa.RestAPI.Data.Products;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace EnviosYa.RestAPI.Endpoints;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/products")
            .WithTags("Products")
            .WithOpenApi();

        productGroup.MapPost("/", async ([FromBody] CreateProductDto request, [FromServices] IValidator<CreateProductDto> validator, [FromServices] ICommandHandler<CreateProductCommand, CreateProductResponseDto> handler) =>
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }
            
            var command = request.ToCommand();
            var result = await handler.Handle( command );

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

    }
}