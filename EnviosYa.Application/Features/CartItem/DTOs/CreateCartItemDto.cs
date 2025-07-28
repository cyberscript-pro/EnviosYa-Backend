using EnviosYa.Application.Features.CartItem.Commands.Create;

namespace EnviosYa.Application.Features.CartItem.DTOs;

public record CreateCartItemDto(
    string CartId,
    string ProductoId,
    int Cantidad,
    Domain.Entities.Product Product,
    Domain.Entities.Cart Cart
    );

public static class CreateCartItemToCommand
{
    public static CreateCartItemCommand MapToCommand(this CreateCartItemDto dto)
    {
        return new CreateCartItemCommand
        {
            CartId = dto.CartId,
            ProductId = dto.ProductoId,
            Cantidad = dto.Cantidad,
            Product = dto.Product,
            Cart = dto.Cart
        };
    }
}