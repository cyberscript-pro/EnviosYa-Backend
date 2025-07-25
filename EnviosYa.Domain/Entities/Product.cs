using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Domain.Entities;

public class Product : AggregateRoot<Guid>
{
    public Product() : base(Guid.NewGuid())
    {}
    
    public Product(Guid id) : base(id)
    {}
    
    public required string Name { get; set; } = default!;
    public string Description { get; set; }  = default!;
    public required double Price { get; set; }
    public required int Stock { get; set; }
    public required CategoryProduct Category { get; set; }
    public required List<string> ImagesUrls { get; set; }
    public bool IsAvailable { get; set; } = true;
}