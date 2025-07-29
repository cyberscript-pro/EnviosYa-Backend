using System.Security.Claims;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Login.Commands.GoogleAuth;
using EnviosYa.Application.Features.Auth.Login.Commands.Login;
using EnviosYa.Application.Features.Auth.Login.Commands.Refresh;
using EnviosYa.Application.Features.Auth.Login.DTOs;
using EnviosYa.Application.Features.Auth.Login.Queries.GetUserByID;
using EnviosYa.Domain.Constants;
using FluentValidation;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;

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
            Tags = new List<OpenApiTag> { new() { Name = "Authentication" } }
        });

        authGroup.MapPost("/refresh", async ([FromBody] RefreshTokenDto dto, [FromServices] IValidator<RefreshTokenDto> validator, [FromServices] ICommandHandler<RefreshTokenUserCommand, RefreshTokenUserResponseDto> handler) =>
            {
                var validateResult = await validator.ValidateAsync(dto);

                if (!validateResult.IsValid)
                {
                    var errors = validateResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    return Results.BadRequest(errors);
                }
                
                var command = dto.MapToCommand();
                var result = await handler.Handle(command);
                
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Refresh Token current user",
            Description = "Refresh Token logged user",
            Tags = new List<OpenApiTag> { new() { Name = "Authentication" } }
        });

        authGroup.MapPost("/google", async ([FromBody] GoogleAuthDto dto, [FromServices] IValidator<GoogleAuthDto> validator, [FromServices] ICommandHandler<GoogleAuthCommand, GoogleAuthResponseDto> handler) =>
        {
            GoogleJsonWebSignature.Payload payload;
            
            var validationResult = await validator.ValidateAsync(dto);
         
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }
            
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

                var command = new GoogleAuthDtoToCommand(
                    FullName: payload.Name,
                    Email: payload.Email,
                    Provider: "Google",
                    ProviderId: payload.Subject,
                    Role: RolUser.Cliente,
                    ProfilePicture: "",
                    Phone: ""
                ).MapToCommand();

                var result = await handler.Handle(command);
                
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            }
            catch (InvalidJwtException)
            {
               return Results.Unauthorized();
            }
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Login a user with Google account", 
            Description = "Login a user through its google account",
            Tags = new List<OpenApiTag> { new() { Name = "Authentication" } }
        });

        authGroup.MapGet("/me", async (HttpContext context, [FromServices] IQueryHandler<GetUserByIDQuery, GetUserByIDResponseDto> handler) =>
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                             ?? context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                {
                    return await Task.FromResult(Results.Unauthorized());
                }

                var result = await handler.Handle(new GetUserByIDQuery
                {
                    Id = Guid.Parse(userId)
                });
                
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new (operation)
        {
            Summary = "Get current user",
            Description = "Get current logged user data",
            Tags = new List<OpenApiTag> { new() { Name = "Authentication" } }
        }).RequireAuthorization();
    }
}