using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Queries.GetFilterCategory;

public sealed record GetFilterCategoryProductResponseDto(
    Guid Id,
    string Name,
    string Description,
    double Price,
    int Stock,
    CategoryProduct Category,
    List<string> Images
);

public static class GetFilterCategoryProductToResponse
{
    public static GetFilterCategoryProductResponseDto MapToResponse(Domain.Entities.Product product)
    {
        return new GetFilterCategoryProductResponseDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.Category,
            product.ImagesUrls
        );
    }
}