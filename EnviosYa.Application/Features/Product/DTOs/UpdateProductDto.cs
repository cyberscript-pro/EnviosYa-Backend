using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Update;

namespace EnviosYa.Application.Features.Product.DTOs;

public record UpdateProductDto(
    string Name,
    double Price,
    int Stock,
    string CategoryId,
    List<string> ProductImages
    );


public static class UpdateProductDtoToCommand
{
    public static UpdateProductCommand ToCommand(this UpdateProductDto dto, Guid id)
    {
        return new UpdateProductCommand
        {
            Id = id,
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = Guid.Parse(dto.CategoryId),
            ProductImages = dto.ProductImages
        };
    }
}