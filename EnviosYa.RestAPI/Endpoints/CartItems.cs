using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.CartItem.Commands.Create;
using EnviosYa.Application.Features.CartItem.DTOs;
using EnviosYa.Application.Features.CartItem.Queries.GetAll;
using FluentValidation;
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
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new (operation)
        {
            Summary = "FindAll a cart items",
            Description = "find all cart items in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "CartItems" } }
        });

        itemsGroup.MapPost("/", async ([FromBody] CreateCartItemDto dto, [FromServices] IValidator<CreateCartItemDto> validator , [FromServices] ICommandHandler<CreateCartItemCommand, CreateCartItemResponseDto> handler) =>
            {
                var validationResult = await validator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    return Results.BadRequest(errors);
                }

                var command = dto.MapToCommand();
                var result =  await handler.Handle(command);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Creates a cart item",
            Description = "Creates a cart item in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "CartItems" } }
        });
    }
}