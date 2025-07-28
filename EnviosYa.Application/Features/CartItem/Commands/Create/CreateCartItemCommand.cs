using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommand : ICommand<CreateCartItemResponseDto>
{
    public string CartId { get; init; }
    public string ProductId { get; init; }
    public int Cantidad { get; init; }
    public Domain.Entities.Product  Product { get; init; }
    public Domain.Entities.Cart  Cart { get; init; }
}