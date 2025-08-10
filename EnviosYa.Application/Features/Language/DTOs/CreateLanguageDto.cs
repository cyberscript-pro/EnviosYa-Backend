using EnviosYa.Application.Features.Language.Commands.Create;

namespace EnviosYa.Application.Features.Language.DTOs;

public record CreateLanguageDto(
    string Code,
    string Name
    );

public static class CreateLanguageToCommand
{
    public static CreateLanguageCommand MapToCommand(this CreateLanguageDto dto)
    {
        return new CreateLanguageCommand
        {
            Code =  dto.Code,
            Name = dto.Name
        };
    }
}