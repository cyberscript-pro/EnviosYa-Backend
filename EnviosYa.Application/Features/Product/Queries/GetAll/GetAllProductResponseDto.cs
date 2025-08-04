using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Queries.GetAll;

public sealed record GetAllProductResponseDto(
        Guid Id,
        double Price,
        int Stock,
        string Category,
        List<string> Images
    );

public static class GetAllProductToResponse
{
    public static GetAllProductResponseDto MapToResponse(Domain.Entities.Product product)
    {
        return new GetAllProductResponseDto(
            product.Id,
            product.Price,
            product.Stock,
            product.Category.Name,
            product.ProductImages.ToList()
            );
    }
}