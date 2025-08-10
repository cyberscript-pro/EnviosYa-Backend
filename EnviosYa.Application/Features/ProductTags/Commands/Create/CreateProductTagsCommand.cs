using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.ProductTagsTranslations.Commands.Create;

namespace EnviosYa.Application.Features.ProductTags.Commands.Create;

public class CreateProductTagsCommand : ICommand<CreateProductTagsResponseDto>
{
    public required Guid ProductId { get; set; }
    public ICollection<CreateProductTagsTranslationsCommand> Translations { get; set; } =  new List<CreateProductTagsTranslationsCommand>(); 
}