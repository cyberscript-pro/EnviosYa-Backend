using EnviosYa.Application.Features.Auth.Login.Commands.Refresh;

namespace EnviosYa.Application.Features.Auth.Login.DTOs;

public record RefreshTokenDto(string RefreshToken);

public static class RefreshTokenUserToResponse
{
    public static RefreshTokenUserCommand MapToCommand(this RefreshTokenDto dto)
    {
        return new RefreshTokenUserCommand
        {
            RefreshToken = dto.RefreshToken,
        };
    }
}