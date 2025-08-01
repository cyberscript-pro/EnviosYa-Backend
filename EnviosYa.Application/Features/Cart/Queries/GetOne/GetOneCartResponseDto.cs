using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Cart.Queries.GetOne;

public record GetOneCartResponseDto(
    Guid Id,
    Guid UserId,
    List<CartItemDto> Items
);

public record CartItemDto(
    Guid Id,
    Guid ProductId,
    int Quantity,
    ProductDto? Product
);

public record ProductDto(
    Guid Id,
    string Name,
    double Price,
    CategoryProduct Category
);