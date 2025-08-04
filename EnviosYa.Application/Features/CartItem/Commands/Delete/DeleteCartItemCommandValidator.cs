using EnviosYa.Application.Features.CartItem.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.CartItem.Commands.Delete;

public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemDto>
{
    public DeleteCartItemCommandValidator(IRepository repository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(async(cartItemDto, id, cancellationToken) =>
            {
                var user = await repository.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == Guid.Parse(cartItemDto.UserId) && u.IsAvailable, cancellationToken);

                if (user is null )
                {
                     return false;
                }
                
                var cartItem = await repository.CartItems.FirstOrDefaultAsync(ci => ci.Id == Guid.Parse(id) && ci.IsAvailable, cancellationToken);

                if (cartItem is null)
                {
                    return false;
                }
                
                return (bool)user.Cart?.Items.Contains(cartItem);
            }).WithMessage("Cart item doesn't exist or delete item unauthorized");
    }
}