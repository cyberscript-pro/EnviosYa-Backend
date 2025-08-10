using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Category.Command.CreateTranslation;

public class CreateCategoryTranslationsCommand : ICommand<CreateCategoryTranslationsResponseDto>
{
    public required Guid CategoryId { get; init; }
    public required Guid LanguageId { get; init; }
    
    public required string Name { get; init; }
}