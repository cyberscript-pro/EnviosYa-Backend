using EnviosYa.Application.Features.ProductTranslations.Command.Create;

namespace EnviosYa.Application.Features.ProductTranslations.DTOs;

public record CreateProductTranslationDto(
    string ProductId,
    string LanguageId,
    string Name,
    string Description
    );
    
public static class CreateProductDtoToCommand
{
    public static CreateProductTranslationsCommand MapToCommand(this CreateProductTranslationDto dto)
    {
        return new CreateProductTranslationsCommand
        {
            ProductId = Guid.Parse(dto.ProductId),
            LanguageId = Guid.Parse(dto.LanguageId),
            Name = dto.Name,
            Description = dto.Description,
        };
    }
}