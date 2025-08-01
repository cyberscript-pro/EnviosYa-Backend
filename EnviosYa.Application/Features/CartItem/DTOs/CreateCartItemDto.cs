using EnviosYa.Application.Features.CartItem.Commands.Create;

namespace EnviosYa.Application.Features.CartItem.DTOs;

public record CreateCartItemDto(
    string ProductoId,
    int Cantidad
    );

public record CreateCartItemDtoToCommand(
    string UserId,
    string ProductoId,
    int Cantidad
    );

public static class CreateCartItemToCommand
{
    public static CreateCartItemCommand MapToCommand(this CreateCartItemDtoToCommand dto)
    {
        return new CreateCartItemCommand
        {
            UserId = dto.UserId,
            ProductId = dto.ProductoId,
            Cantidad = dto.Cantidad,
        };
    }
}