using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Auth.Login.Commands.Refresh;

public class RefreshTokenUserCommand :  ICommand<RefreshTokenUserResponseDto>
{
    public required string RefreshToken { get; set; }
}