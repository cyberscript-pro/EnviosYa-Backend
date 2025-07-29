namespace EnviosYa.Application.Common.Abstractions;

public interface IRefreshTokenHasher
{
    public string Hash(string refreshToken);
}