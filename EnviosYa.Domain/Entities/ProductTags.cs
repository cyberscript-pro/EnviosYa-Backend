using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductTags : AggregateRoot<Guid>
{
    public ProductTags() : base(Guid.NewGuid())
    {}
    
    public ProductTags(Guid id) : base(id)
    {}
    
    public required Guid ProductId { get; set; }
    public required Product Product { get; set; }
    
    public ICollection<ProductTagsTranslations> Translations { get; set; }
    
    public bool IsAvailable { get; set; } = true;
}