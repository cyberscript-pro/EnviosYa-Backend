namespace EnviosYa.Application.Features.ProductTagsTranslations.Commands.Create;

public class CreateProductTagsTranslationsCommand
{
    public required Guid ProductTagsId { get; set; }
    public required Guid LanguageId { get; set; }
    public required string Name { get; set; }
}