using System.Security.Claims;
using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Cart.Queries.GetAll;
using EnviosYa.Application.Features.Cart.Queries.GetOne;
using EnviosYa.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EnviosYa.RestAPI.Endpoints;

public static class Cart
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        var cartGroup = app.MapGroup("/cart")
            .WithTags("Cart")
            .WithOpenApi();

        cartGroup.MapGet("/", async ([FromServices] IQueryHandler<GetAllCartQuery, List<GetAllCartResponseDto>> handler) =>
            {
                var result = await handler.Handle(new GetAllCartQuery());

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "FindAll a cart",
            Description = "find all cart in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Cart" } }
        }).RequireAuthorization(policy => policy.RequireRole(nameof(RolUser.Admin)));

        cartGroup.MapGet("/me", async (HttpContext context, [FromServices] IQueryHandler<GetOneCartQuery, GetOneCartResponseDto> handler) =>
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                         ?? context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            
            if (string.IsNullOrEmpty(userId))
            {
                return await Task.FromResult(Results.Unauthorized());
            }

            var result = await handler.Handle(new GetOneCartQuery
            {
                Id = Guid.Parse(userId)
            });
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Find a cart in the system",
            Description = "find cart by id in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Cart" } }
        }).RequireAuthorization();

    }
}