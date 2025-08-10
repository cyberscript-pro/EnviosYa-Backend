using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductTranslations : AggregateRoot<Guid>
{
    public ProductTranslations() : base(Guid.NewGuid())
    {}
    
    public ProductTranslations(Guid id) : base(id)
    {}

    
    public required Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    
    public required Guid LanguageId { get; set; }
    public Language Language { get; set; } = default!;
    
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsAvailable { get; set; } = true;
}