using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommandHandler(IRepository repository) : ICommandHandler<CreateCartItemCommand, CreateCartItemResponseDto>
{
    public async Task<Result<CreateCartItemResponseDto>> Handle(CreateCartItemCommand command, CancellationToken cancellationToken = default)
    {
        var user = await repository.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == Guid.Parse(command.UserId), cancellationToken);

        if (user?.Cart is null)
        {
            return await Task.FromResult(Result.Failure<CreateCartItemResponseDto>(Error.NotFound("400", "User not found")));
        }
        
        var cartItem = new Domain.Entities.CartItem
        {
            ProductId = Guid.Parse(command.ProductId),
            CartId = user.Cart.Id,
            Cantidad = command.Cantidad,
        };
        
        repository.CartItems.Add(cartItem);
        
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(
            Result.Success(new CreateCartItemResponseDto(
                ProductId: command.ProductId,
                Quantity: command.Cantidad,
                CartId: user.Cart.Id.ToString()
            ))
        );
    }
}