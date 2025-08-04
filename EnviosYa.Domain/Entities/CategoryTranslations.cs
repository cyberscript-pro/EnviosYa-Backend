using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class CategoryTranslations : AggregateRoot<Guid>
{
    public CategoryTranslations() : base(Guid.NewGuid())
    {}
    
    public CategoryTranslations(Guid id) : base(id)
    {}
    
    public required Guid CategoryId { get; set; }
    public required Category Category { get; set; }
    
    public required Guid LanguageId { get; set; }
    public required Language Language { get; set; }
    
    public required string Name { get; set; }
    public bool IsAvailable { get; set; } = true;
}