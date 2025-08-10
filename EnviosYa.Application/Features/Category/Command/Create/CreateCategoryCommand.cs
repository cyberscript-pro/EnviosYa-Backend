using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Category.Command.Create;

public class CreateCategoryCommand : ICommand<CreateCategoryResponseDto>
{
    public required Guid LanguageId { get; init; }
    public required string Name { get; init; }
}