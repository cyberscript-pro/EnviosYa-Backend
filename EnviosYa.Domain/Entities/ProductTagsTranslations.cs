using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductTagsTranslations : AggregateRoot<Guid>
{
    public ProductTagsTranslations() : base(Guid.NewGuid())
    {}
    
    public ProductTagsTranslations(Guid id) : base(id)
    {}
    
    public required Guid ProductTagsId { get; set; }
    public required ProductTags ProductTags { get; set; } = default!;
    
    public required Guid LanguageId { get; set; }
    public required Language Language { get; set; }
    
    public required string Name { get; set; }
    public bool IsAvailable { get; set; } = true;
}