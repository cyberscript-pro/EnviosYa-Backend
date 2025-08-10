using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Commands.Update;

public sealed record UpdateProductResponseDto(
    Guid Id,
    string Name,
    double Price,
    int Stock,
    CategoryProduct Category,
    List<string> ImageUrl
    );