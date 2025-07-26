using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Login.Commands;
using EnviosYa.Application.Features.Auth.Login.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class AuthEndpoint
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        authGroup.MapPost("/login", async ([FromBody] LoginUserDto dto, [FromServices] IValidator<LoginUserDto> validator, [FromServices] ICommandHandler<LoginUserCommand, LoginUserResponseDto> handler) =>
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
        .WithOpenApi(operation => new (operation)
        {
            Summary = "Login a user", 
            Description = "Login a user through its credentials",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
        });

        authGroup.MapGet("/me", async () =>
        {

        })
        .WithOpenApi(operation => new (operation)
        {
            Summary = "Get current freelancer",
            Description = "Get current logged freelancer user data",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
        });
    }
}