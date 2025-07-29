namespace EnviosYa.Application.Common.Abstractions;

public interface IRefreshTokenGenerator
{
    public string GenerateRefreshToken();
}