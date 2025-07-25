using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Common.Abstractions;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}