using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.ProductSpecificationTranslations.Command.Create;

public class CreateProductSpecificationTranslationsCommand : ICommand<CreateProductSpecificationTranslationsResponseDto>
{
    public required Guid ProductSpecificationId { get; init; }
    public required Guid LanguageId { get; init; }
    public required string Key { get; init; }
    public required string Value { get; init; }
}