using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommand : ICommand<CreateCartItemResponseDto>
{
    public required string CartId { get; init; }
    public required string ProductId { get; init; }
    public int Cantidad { get; init; }
    public required Domain.Entities.Product  Product { get; init; }
    public required Domain.Entities.Cart  Cart { get; init; }
}