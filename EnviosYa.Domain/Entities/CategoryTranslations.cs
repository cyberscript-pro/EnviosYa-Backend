using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class CategoryTranslations : AggregateRoot<Guid>
{
    public CategoryTranslations() : base(Guid.NewGuid())
    {}
    
    public CategoryTranslations(Guid id) : base(id)
    {}
    
    public required Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    
    public required Guid LanguageId { get; set; }
    public Language Language { get; set; } = default!;
    
    public required string Name { get; set; }
    public bool IsAvailable { get; set; } = true;
}