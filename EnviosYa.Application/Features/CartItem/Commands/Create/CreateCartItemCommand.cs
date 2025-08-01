using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommand : ICommand<CreateCartItemResponseDto>
{
    public required string UserId { get; set; }
    public required string ProductId { get; set; }
    public int Cantidad { get; set; }
    public Domain.Entities.Product?  Product { get; set; }
    public Domain.Entities.Cart?  Cart { get; set; }
}