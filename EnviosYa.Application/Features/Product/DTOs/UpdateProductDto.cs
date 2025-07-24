using System.Globalization;
using System.Text;
using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.DTOs;

public record UpdateProductDto(
    string Name,
    string Description,
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
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = category,
            ImagesUrls = dto.ImagesUrls
        };
    }
}