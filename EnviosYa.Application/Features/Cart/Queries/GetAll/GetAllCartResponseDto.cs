using EnviosYa.Domain.Constants;
using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Features.Cart.Queries.GetAll;

public sealed record GetAllCartResponseDto(
    Guid Id,
    Guid UserId,
    List<Domain.Entities.CartItem> Items
    );

public class GetAllCartToResponse
{
    public static GetAllCartResponseDto MapToResponse(Domain.Entities.Cart cart)
    {
        return new GetAllCartResponseDto(
            cart.Id,
            cart.UserId,
            cart.Items
        );
    }
}