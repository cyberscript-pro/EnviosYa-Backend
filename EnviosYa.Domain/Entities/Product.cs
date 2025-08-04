using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Domain.Entities;

public class Product : AggregateRoot<Guid>
{
    public Product() : base(Guid.NewGuid())
    {}
    
    public Product(Guid id) : base(id)
    {}
    
    public required double DiscountPrice { get; set; }
    public required double Price { get; set; }
    public required int Stock { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAvailable { get; set; } = true;
    
    public required Guid CategoryId { get; set; }
    public required Category Category { get; set; } = default!;
    public ICollection<string> ProductImages { get; set; } = new List<string>();
    public ICollection<ProductTranslations> Translations { get; set; } = new List<ProductTranslations>();
    public ICollection<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>(); 
    public ICollection<ProductTags> Tags { get; set; }  = new List<ProductTags>();
    public ICollection<ProductReviews> Reviews { get; set; }  = new List<ProductReviews>();
}