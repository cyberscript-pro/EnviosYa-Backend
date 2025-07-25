using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Features.Cart.Queries.GetAll;

public sealed record GetAllCartResponseDto(
    Guid Id,
    Guid UserId,
    User User,
    List<Domain.Entities.CartItem> CartItems
    );

public class GetAllCartToResponse
{
    public static GetAllCartResponseDto MapToResponse(Domain.Entities.Cart cart)
    {
        return new GetAllCartResponseDto(
            cart.Id,
            cart.UserId,
            cart.User,
            cart.Items
        );
    }
}