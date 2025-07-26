using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Register.Commands.Create;
using EnviosYa.Application.Features.Auth.Register.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class Users
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var usersGroup = app.MapGroup("/users")
            .WithTags("Users")
            .WithOpenApi();

        usersGroup.MapPost("/register", async ([FromBody] CreateUserDto dto, [FromServices] IValidator<CreateUserDto> validator, [FromServices] ICommandHandler<CreateUserCommand, CreateUserResponseDto> handler) =>
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
            Summary = "Creates a user",
            Description = "Creates a user in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Users" } }
        });
    }
}