using System.Globalization;
using System.Text;
using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.DTOs;

public record CreateProductDto( 
    string Name,
    double Price,
    int Stock,
    string Category,
    List<string> ImagesUrls
);

public static class CreateProductDtoToCommand
{
    public static CreateProductCommand ToCommand(this CreateProductDto dto)
    {
        CategoryMapper.TryParseCategory(dto.Category, out var category);
        
        return new CreateProductCommand
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = category,
            ImagesUrls = dto.ImagesUrls
        };
    }
}