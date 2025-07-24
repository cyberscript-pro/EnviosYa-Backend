using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Commands.Update;

public sealed class UpdateProductCommand: ICommand<UpdateProductResponseDto>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public required double Price { get; init; }
    public required int Stock { get; init; }
    public required CategoryProduct Category { get; init; }
    public List<string> ImagesUrls { get; init; } = new();
}