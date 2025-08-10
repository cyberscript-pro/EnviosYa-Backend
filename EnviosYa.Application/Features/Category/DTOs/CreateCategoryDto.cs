using EnviosYa.Application.Features.Category.Command.Create;

namespace EnviosYa.Application.Features.Category.DTOs;

public record CreateCategoryDto(
    string LanguageId,
    string Name
    );

public static class CreateCategoryToCommand
{
    public static CreateCategoryCommand MapToCommand(this CreateCategoryDto dto)
    {
        return new CreateCategoryCommand
        {
            LanguageId = Guid.Parse(dto.LanguageId),
            Name = dto.Name
        };
    }
}