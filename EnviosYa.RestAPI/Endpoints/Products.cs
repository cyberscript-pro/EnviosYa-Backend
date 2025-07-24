using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Application.Features.Product.Commands.Delete;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Application.Features.Product.Queries.GetAll;
using EnviosYa.Application.Features.Product.Queries.GetFilterCategory;
using EnviosYa.Application.Features.Product.Queries.GetOne;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/products")
            .WithTags("Products")
            .WithOpenApi();

        productGroup.MapGet("/", async ([FromServices] IQueryHandler<GetAllProductQuery, List<GetAllProductResponseDto>> handler) =>
        {
            var result = await handler.Handle(new GetAllProductQuery());

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
            .WithOpenApi(operation => new(operation)
        {
            Summary = "FindAll a products",
            Description = "find all products in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Products" } }
        });

        productGroup.MapGet("/{id}", async ([FromServices] IQueryHandler<GetOneProductQuery, GetOneProductResponseDto> handler, string id) =>
            {
                var query = new GetOneProductQuery
                {
                    Id = Guid.Parse(id)
                };

                var result = await handler.Handle(query);
                
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Find a product",
            Description = "Find a product in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Products" } }
        });

        productGroup.MapGet("/filter/{category}", async ([FromServices] IValidator<GetCategoryProductDto> validator,[FromServices] IQueryHandler<GetFilterCategoryProductQuery,List<GetFilterCategoryProductResponseDto>> handler, string category) =>
        {
            var validationResult = await validator.ValidateAsync(new GetCategoryProductDto(category));

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }

            var productCategory = new GetCategoryProductDto(category);

            var query = productCategory.ToCommand();
            var result = await handler.Handle(query);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

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
        })
            .WithOpenApi(operation => new(operation)
        {
            Summary = "Creates a product",
            Description = "Creates a product in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Products" } }
        });

        productGroup.MapPatch("/{id}", async ([FromBody] UpdateProductDto request, [FromServices] IValidator<UpdateProductDto> validator, [FromServices] ICommandHandler<UpdateProductCommand, UpdateProductResponseDto> handler, string id) =>
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }

            var command = request.ToCommand(Guid.Parse(id));
            var result = await handler.Handle(command);
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates a product",
            Description = "Updates a product in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Products" } }
        });
        
        productGroup.MapDelete("/{id}", async ([FromServices] ICommandHandler<DeleteProductCommand, DeleteProductResponseDto> handler, string id) => 
        {

        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Deletes a product",
            Description = "Deletes a product in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Products" } }
        });

    }
}