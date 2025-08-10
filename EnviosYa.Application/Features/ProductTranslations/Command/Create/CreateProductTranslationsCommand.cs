using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.ProductTranslations.Command.Create;

public class CreateProductTranslationsCommand : ICommand<CreateProductTranslationsResponseDto>
{
    public required Guid ProductId { get; init; }
    public required Guid LanguageId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}