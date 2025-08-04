using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Constants;
using OneOf;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public record CreateProductCommand : ICommand<CreateProductResponseDto>
{
    public required string Name { get; init; }
    public required double Price { get; init; }
    public required int Stock { get; init; }
    public required CategoryProduct Category { get; init; }
    public List<string> ImagesUrls { get; init; } = new();
}