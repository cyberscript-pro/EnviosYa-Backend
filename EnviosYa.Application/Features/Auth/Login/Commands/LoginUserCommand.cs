using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Auth.Login.Commands;

public class LoginUserCommand : ICommand<LoginUserResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}