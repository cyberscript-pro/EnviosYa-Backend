using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Product.Commands.Delete;

public class DeleteProductCommand : ICommand<DeleteProductResponseDto>
{
    public required Guid Id { get; init; }
}