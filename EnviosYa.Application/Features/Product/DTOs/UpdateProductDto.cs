using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Update;

namespace EnviosYa.Application.Features.Product.DTOs;

public record UpdateProductDto(
    string Name,
    double Price,
    int Stock,
    string Category,
    List<string> ImagesUrls
    );


public static class UpdateProductDtoToCommand
{
    public static UpdateProductCommand ToCommand(this UpdateProductDto dto, Guid id)
    {
        CategoryMapper.TryParseCategory(dto.Category, out var category);
        
        return new UpdateProductCommand
        {
            Id = id,
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = category,
            ImagesUrls = dto.ImagesUrls
        };
    }
}