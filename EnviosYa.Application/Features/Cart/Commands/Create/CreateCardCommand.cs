using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Features.Cart.Commands.Create;

public class CreateCardCommand : ICommand<CreateCartResponseDto>
{
    public required Guid UserId { get; set; }
    public required User User { get; set; }
}