using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class ProductReviews : AggregateRoot<Guid>
{
    public ProductReviews() : base(Guid.NewGuid())
    {}
    
    public ProductReviews(Guid id) : base(id)
    {}
    
    public required Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public required Guid UserId { get; set; }
    public required User User { get; set; }
    public required int Rating { get; set; }
    public string Comment { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAvailable { get; set; } = true;
}