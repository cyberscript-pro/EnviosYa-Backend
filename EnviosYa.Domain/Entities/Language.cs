using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class Language : AggregateRoot<Guid>
{
    public Language() : base(Guid.NewGuid())
    {}
    
    public Language(Guid id) : base(id)
    {}

    public required string Code { get; set; }
    public required string Name { get; set; }

    public ICollection<ProductTranslations> ProductTranslations { get; set; } = new List<ProductTranslations>();
    public ICollection<ProductSpecificationTranslations> ProductSpecificationTranslations { get; set; } = new List<ProductSpecificationTranslations>();
    public ICollection<ProductTagsTranslations> ProductTagsTranslations { get; set; } = new List<ProductTagsTranslations>();
    public ICollection<CategoryTranslations> CategoryTranslations { get; set; } = new List<CategoryTranslations>();
}