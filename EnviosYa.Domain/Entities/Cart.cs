using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class Cart: AggregateRoot<Guid>
{
    public Cart(): base(Guid.NewGuid())
    {}
    
    public Cart(Guid id):  base(id)
    {}
    
    public required Guid UserId { get; set; }
    public required User User { get; set; } = default!;

    public required List<CartItem> Items { get; set; } = new();
}