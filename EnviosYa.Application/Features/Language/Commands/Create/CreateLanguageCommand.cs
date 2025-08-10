using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Language.Commands.Create;

public class CreateLanguageCommand : ICommand<CreateLanguageResponseDto>
{
    public required string Code { get; init; }
    public required string Name { get; init; }
}