using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Auth.Register.Commands.Create;

public class CreateUserCommand : ICommand<CreateUserResponseDto>
{
    public required string FullName { get; set; }
    public required string Nickname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public RolUser? Role { get; set; } = RolUser.Cliente;
    
    public string? ProfilePicture { get; set; }
    public string? Phone { get; set; }
    
    public Domain.Entities.Cart? Cart { get; set; }
}