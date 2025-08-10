using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.ProductSpecification.Command.Create;
using EnviosYa.Application.Features.ProductSpecificationTranslations.Command.Create;
using EnviosYa.Application.Features.ProductTags.Commands.Create;
using EnviosYa.Application.Features.ProductTagsTranslations.Commands.Create;
using EnviosYa.Application.Features.ProductTranslations.Command.Create;
using EnviosYa.Domain.Constants;
using OneOf;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public record CreateProductCommand : ICommand<CreateProductResponseDto>
{
    public required double DiscountPrice { get; init; }
    public required double Price { get; init; }
    public required int Stock { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid LanguageId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ICollection<string> ProductImages { get; init; } = new List<string>();
    public ICollection<CreateProductTranslationsCommand> Translations { get; init; } = new List<CreateProductTranslationsCommand>();
    public ICollection<CreateProductSpecificationCommand> Specifications { get; init; } = new List<CreateProductSpecificationCommand>();
    public ICollection<CreateProductSpecificationTranslationsCommand> SpecificationTranslations { get; init; } = new List<CreateProductSpecificationTranslationsCommand>();
    public ICollection<CreateProductTagsCommand> Tags { get; init; } = new List<CreateProductTagsCommand>();
    public ICollection<CreateProductTagsTranslationsCommand> TagsTranslations { get; init; } = new List<CreateProductTagsTranslationsCommand>();
}