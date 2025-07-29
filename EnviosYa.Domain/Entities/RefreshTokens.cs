using EnviosYa.Domain.Common;

namespace EnviosYa.Domain.Entities;

public class RefreshToken : AggregateRoot<Guid>
{
    public RefreshToken() : base(Guid.NewGuid())
    {}
    
    public RefreshToken(Guid id) : base(id)
    {}
    
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public User User { get; set; } = default!;
}