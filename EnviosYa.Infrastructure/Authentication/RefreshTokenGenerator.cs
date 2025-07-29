using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Infrastructure.Authentication;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N");
    }
}