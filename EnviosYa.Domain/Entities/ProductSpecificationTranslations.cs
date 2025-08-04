using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductSpecificationTranslations : AggregateRoot<Guid>
{
    public ProductSpecificationTranslations() : base(Guid.NewGuid())
    {}
    
    public ProductSpecificationTranslations(Guid id) : base(id)
    {}
    
    public required Guid ProductSpecificationId { get; set; }
    public required ProductSpecification ProductSpecification { get; set; }
    
    public required Guid LanguageId { get; set; }
    public required Language Language { get; set; }
    
    public required string Key { get; set; }
    public required string Value { get; set; } 
    public bool IsAvailable { get; set; } = true;
}