using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommandHandler(IRepository repository) : ICommandHandler<CreateCartItemCommand, CreateCartItemResponseDto>
{
    public async Task<Result<CreateCartItemResponseDto>> Handle(CreateCartItemCommand command, CancellationToken cancellationToken = default)
    {
        var cartItem = new Domain.Entities.CartItem
        {
            ProductId = Guid.Parse(command.ProductId),
            CartId = Guid.Parse(command.CartId),
            Cantidad = command.Cantidad,
            Producto = command.Product,
            Cart = command.Cart,
        };
        
        repository.CartItems.Add(cartItem);
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(
            Result.Success(new CreateCartItemResponseDto(
                ProductId: command.ProductId,
                Quantity: command.Cantidad,
                CartId: command.CartId
            ))
        );
    }
}