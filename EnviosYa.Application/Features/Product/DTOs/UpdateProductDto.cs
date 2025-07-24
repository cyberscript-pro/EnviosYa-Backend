using EnviosYa.Application.Features.Product.Commands.Update;

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
    public static UpdateProductCommand ToCommand(this UpdateProductDto updateProductDto, Guid id)
    {
        return new UpdateProductCommand();
    }
}