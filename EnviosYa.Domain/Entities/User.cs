using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    protected User() : base(Guid.NewGuid())
    {}
    
    protected User(Guid id) : base(id)
    {}
    
    public required string FullName { get; set; }
    public required string Nickname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required RolUser Role { get; set; } = RolUser.Cliente;
    
    public string? ProfilePicture { get; set; }
    public string? Phone { get; set; }
    public bool IsAvailable { get; set; } = true;
    
    public Cart? Cart { get; set; }
}