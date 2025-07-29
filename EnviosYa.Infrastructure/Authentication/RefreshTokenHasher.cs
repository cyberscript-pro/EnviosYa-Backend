using System.Security.Cryptography;
using System.Text;
using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Infrastructure.Authentication;

public class RefreshTokenHasher : IRefreshTokenHasher
{
    public string Hash(string refreshToken)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
        return Convert.ToHexString(bytes);
    }
}