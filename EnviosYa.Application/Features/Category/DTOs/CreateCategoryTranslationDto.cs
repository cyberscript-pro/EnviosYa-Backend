using EnviosYa.Application.Features.Category.Command.CreateTranslation;

namespace EnviosYa.Application.Features.Category.DTOs;

public record CreateCategoryTranslationDto(
    string Name,
    string LanguageId,
    string CategoryId
    );

public static class CreateCategoryTranslationToCommand
{
    public static CreateCategoryTranslationsCommand MapToCommand(this CreateCategoryTranslationDto dto)
    {
        return new CreateCategoryTranslationsCommand
        {
            Name = dto.Name,
            LanguageId = Guid.Parse(dto.LanguageId),
            CategoryId = Guid.Parse(dto.CategoryId)
        };
    }
}