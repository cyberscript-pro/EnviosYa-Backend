using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Queries.GetFilterCategory;

public sealed record GetFilterCategoryProductResponseDto(
    Guid Id,
    double Price,
    int Stock,
    string Category,
    List<string> Images
);

public static class GetFilterCategoryProductToResponse
{
    public static GetFilterCategoryProductResponseDto MapToResponse(Domain.Entities.Product product)
    {
        return new GetFilterCategoryProductResponseDto(
            product.Id,
            product.Price,
            product.Stock,
            product.Category.Name,
            product.ProductImages.ToList()
        );
    }
}