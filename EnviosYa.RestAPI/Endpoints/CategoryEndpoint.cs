using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Category.Command.Create;
using EnviosYa.Application.Features.Category.Command.CreateTranslation;
using EnviosYa.Application.Features.Category.DTOs;
using EnviosYa.Application.Features.Category.Queries.GetAll;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace EnviosYa.RestAPI.Endpoints;

public static class CategoryEndpoint
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var categoryGroup = app.MapGroup("/category")
            .WithTags("Category")
            .WithOpenApi();

        categoryGroup.MapGet("/", async ([FromServices] IQueryHandler<GetAllCategoryQuery, List<GetAllCategoryResponseDto>> handler) => 
        {
            var result = await handler.Handle(new GetAllCategoryQuery());

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "FindAll a categories",
            Description = "find all categories in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Category" } }
        });
        
        categoryGroup.MapPost("/", async ([FromBody] CreateCategoryDto dto, [FromServices] IValidator<CreateCategoryDto> validator, [FromServices] ICommandHandler<CreateCategoryCommand, CreateCategoryResponseDto> handler) =>
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }
            
            var command = dto.MapToCommand();
            var result = await handler.Handle(command);
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Creates a category",
            Description = "Creates a category in the system",
            Tags = new List<OpenApiTag> { new() { Name = "Category" } }
        });
        
        categoryGroup.MapPost("/translation", async ([FromBody] CreateCategoryTranslationDto dto, [FromServices] IValidator<CreateCategoryTranslationDto> validator, [FromServices] ICommandHandler<CreateCategoryTranslationsCommand, CreateCategoryTranslationsResponseDto> handler) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }
            
            var command = dto.MapToCommand();
            var result = await handler.Handle(command);
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Creates a category translation",
            Description = "Creates a category translation in the system",
            Tags = new List<OpenApiTag> { new() { Name = "Category" } }
        });
    }
}