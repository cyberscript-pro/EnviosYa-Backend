using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Queries.GetOne;

public sealed record GetOneProductResponseDto(
    Guid Id,
    string Name,
    string Description,
    double Price,
    int Stock,
    CategoryProduct Category,
    List<string> Images
);

public static class GetOneProductToResponse
{
    public static GetOneProductResponseDto MapToResponse(Domain.Entities.Product product)
    {
        return new GetOneProductResponseDto(
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