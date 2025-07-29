using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Common.Abstractions;

public interface IAccessTokenGenerator
{
    public string GenerateToken(User user);
}