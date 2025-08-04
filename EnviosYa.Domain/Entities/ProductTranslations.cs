using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductTranslations : AggregateRoot<Guid>
{
    public ProductTranslations() : base(Guid.NewGuid())
    {}
    
    public ProductTranslations(Guid id) : base(id)
    {}

    
    public required Guid ProductId { get; set; }
    public required Product Product { get; set; }
    
    public required Guid LanguageId { get; set; }
    public required Language Language { get; set; }
    
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsAvailable { get; set; } = true;
}