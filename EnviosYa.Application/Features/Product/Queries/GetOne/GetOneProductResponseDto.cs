using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Queries.GetOne;

public sealed record GetOneProductResponseDto(
    Guid Id,
    double Price,
    int Stock,
    string Category,
    List<string> Images
);

public static class GetOneProductToResponse
{
    public static GetOneProductResponseDto MapToResponse(Domain.Entities.Product product)
    {
        return new GetOneProductResponseDto(
            product.Id,
            product.Price,
            product.Stock,
            product.Category.Name,
            product.ProductImages.ToList()
        );
    }
}