using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.CartItem.Commands.Delete;

public class DeleteCartItemCommandHandler(IRepository repository) : ICommandHandler<DeleteCartItemCommand, DeleteCartItemResponseDto>
{
    public async Task<Result<DeleteCartItemResponseDto>> Handle(DeleteCartItemCommand command, CancellationToken cancellationToken = default)
    {
        var cartItem = await repository.CartItems.FirstOrDefaultAsync(ci => ci.Id == command.Id && ci.IsAvailable, cancellationToken);

        if (cartItem is null)
        {
            return await Task.FromResult(Result.Failure<DeleteCartItemResponseDto>(Error.NotFound("400", "CartItem not found")));
        }
        
        cartItem.IsAvailable = false;
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new DeleteCartItemResponseDto("CartItem was deleted.")));
    }
}