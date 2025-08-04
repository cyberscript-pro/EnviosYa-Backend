using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class Category : AggregateRoot<Guid>
{
    public Category() : base(Guid.NewGuid())
    {}
    
    public Category(Guid id) : base(id)
    {}
    
    public ICollection<Product>  Products { get; set; } = new List<Product>();
    public ICollection<CategoryTranslations>  CategoryTranslations { get; set; } = new List<CategoryTranslations>();
}