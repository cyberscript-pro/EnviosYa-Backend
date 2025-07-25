using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Features.CartItem.Queries.GetAll;

public sealed record GetAllCartItemResponseDto(
    Guid Id,
    Guid ProductId,
    Guid CartId,
    int Cantidad,
    Domain.Entities.Product product,
    Domain.Entities.Cart? cart
    );

public static class GetAllCartItemToResponse
{
    public static GetAllCartItemResponseDto MapToResponse(Domain.Entities.CartItem cartItem)
    {
        return new GetAllCartItemResponseDto(
            cartItem.Id,
            cartItem.ProductId,
            cartItem.CartId,
            cartItem.Cantidad,
            cartItem.Producto,
            cartItem.Cart
        );
    }
}