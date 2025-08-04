using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductSpecification : AggregateRoot<Guid>
{
    public ProductSpecification() : base(Guid.NewGuid())
    {}
    
    public ProductSpecification(Guid id) : base(id)
    {}
    
    public required Guid ProductId { get; set; }
    public required Product Product { get; set; }
    
    public required ICollection<ProductSpecificationTranslations> Translations { get; set; }
    
    public bool IsAvailable { get; set; } = true;
}