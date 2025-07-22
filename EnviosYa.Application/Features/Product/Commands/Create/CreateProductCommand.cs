using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public record CreateProductCommand : ICommand<string>
{
    // public required string Name { get; init; }
    // public string Description { get; init; } = string.Empty;
    // public required double Price { get; init; }
    // public required int Stock { get; init; }
    // public required CategoryProduct Category { get; init; }
    // public List<string> ImagesUrls { get; init; } = new();
}