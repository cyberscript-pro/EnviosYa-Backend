using EnviosYa.Application.Features.Auth.Register.Commands.Create;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Auth.Register.DTOs;

public record CreateUserDto(
    string FullName,
    string Email,
    string Password,
    RolUser Role,
    string? ProfilePicture,
    string? Phone
    );

public static class CreateUserDtoToCommand
{
    public static CreateUserCommand MapToCommand(this CreateUserDto dto)
    {
        return new CreateUserCommand
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,
            ProfilePicture = dto.ProfilePicture,
            Phone = dto.Phone
        };
    }
}