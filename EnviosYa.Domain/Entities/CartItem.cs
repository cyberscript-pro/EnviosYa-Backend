using System.Text.Json.Serialization;
using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class CartItem: AggregateRoot<Guid>
{
    public CartItem():  base(Guid.NewGuid())
    {}
    
    public CartItem(Guid id): base(id)
    {}
    
    public required Guid CartId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Cantidad { get; set; }
    public Product? Producto { get; set; } =  default!;
    [JsonIgnore]
    public Cart? Cart { get; set; }
    public bool IsAvailable { get; set; } = true;
}