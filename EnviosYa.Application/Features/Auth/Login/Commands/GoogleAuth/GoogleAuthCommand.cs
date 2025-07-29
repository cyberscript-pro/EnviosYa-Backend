using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Auth.Login.Commands.GoogleAuth;

public class GoogleAuthCommand : ICommand<GoogleAuthResponseDto>
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string ProviderId { get; set; }
    public RolUser Role { get; set; } = RolUser.Cliente;
    public Provider Provider { get; set; } = Provider.Google;
    public string? ProfilePicture { get; set; }
    public string? Phone { get; set; }
    public Domain.Entities.Cart? Cart { get; set; }
}