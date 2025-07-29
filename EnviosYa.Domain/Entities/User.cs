using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    public User() : base(Guid.NewGuid())
    {}
    
    public User(Guid id) : base(id)
    {}
    
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public RolUser Role { get; set; } = RolUser.Cliente;
    public string? ProfilePicture { get; set; }
    public string? Phone { get; set; }
    public Provider? Provider { get; set; } = Constants.Provider.Credentials;
    public string? ProviderId { get; set; }
    public bool IsNewUser { get; set; } = true;
    public bool IsAvailable { get; set; } = true;
    
    public Cart? Cart { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}