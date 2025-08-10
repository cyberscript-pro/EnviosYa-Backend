using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.CartItem.Commands.Delete;

public class DeleteCartItemCommand : ICommand<DeleteCartItemResponseDto>
{
    public required Guid Id { get; set; }
}