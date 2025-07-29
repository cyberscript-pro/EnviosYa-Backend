using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Auth.Login.Commands.GoogleAuth;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Auth.Login.DTOs;

public record GoogleAuthDto(
    string IdToken
);

public record GoogleAuthDtoToCommand(
    string FullName,
    string Email,
    string Provider,
    string ProviderId,
    RolUser Role,
    string? ProfilePicture,
    string? Phone
    );

public static class GoogleAuthToCommand
{
    public static GoogleAuthCommand MapToCommand(this GoogleAuthDtoToCommand dto)
    {
        ProviderMapper.TryParseProvider(dto.Provider, out var provider);
        
        return new GoogleAuthCommand
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Provider = provider,
            ProviderId = dto.ProviderId,
            Role = dto.Role,
            ProfilePicture = dto.ProfilePicture,
            Phone = dto.Phone
        };
    }
}