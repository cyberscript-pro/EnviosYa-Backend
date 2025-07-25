using EnviosYa.Application.Features.Auth.Login.Commands;

namespace EnviosYa.Application.Features.Auth.Login.DTOs;

public record LoginUserDto(
    string Email,
    string Password
    );

public static class LoginUserToCommand
{
    public static LoginUserCommand MapToCommand(this LoginUserDto dto)
    {
        return new LoginUserCommand
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }
}