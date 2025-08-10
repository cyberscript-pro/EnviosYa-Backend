using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Language;
using EnviosYa.Application.Features.Language.Commands.Create;
using EnviosYa.Application.Features.Language.DTOs;
using EnviosYa.Application.Features.Language.Queries.GetAll;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class LanguageEndpoint
{
    public static void MapLanguageEndpoints(this WebApplication app)
    {
        var languageGroup = app.MapGroup("/language")
            .WithTags("Languages")
            .WithOpenApi();

        languageGroup.MapGet("/",
                async ([FromServices] IQueryHandler<GetAllLanguageQuery, List<GetAllLanguageResponseDto>> handler) =>
                {
                    var result = await handler.Handle(new GetAllLanguageQuery());

                    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "FindAll a languages",
                Description = "find all languages in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Languages" } }
            });

        languageGroup.MapPost("/",
                async ([FromBody] CreateLanguageDto dto, [FromServices] IValidator<CreateLanguageDto> validator,
                    [FromServices] ICommandHandler<CreateLanguageCommand, CreateLanguageResponseDto> handler) =>
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
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Creates a language",
                Description = "Creates a language in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Languages" } }
            });
    }
}