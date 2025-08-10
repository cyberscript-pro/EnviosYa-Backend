using System.Globalization;
using System.Text;
using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.DTOs;

public record CreateProductDto(
    string LanguageId,
    string CategoryId,
    string Name,
    string Description,
    double DiscountPrice,
    double Price,
    int Stock,
    List<string> ProductImages
);

public static class CreateProductDtoToCommand
{
    public static CreateProductCommand ToCommand(this CreateProductDto dto)
    {
        return new CreateProductCommand
        {
            LanguageId = Guid.Parse(dto.LanguageId),
            Name = dto.Name,
            Description = dto.Description,
            DiscountPrice = dto.DiscountPrice,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = Guid.Parse(dto.CategoryId),
            ProductImages = dto.ProductImages
        };
    }
}