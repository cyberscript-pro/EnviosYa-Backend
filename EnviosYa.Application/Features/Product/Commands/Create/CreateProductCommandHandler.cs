using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResponseDto>
{
    public async Task<Result<string>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        // var product = new Domain.Entities.Product
        // {
        // Name = command.Name,
        // Description = command.Description,
        // Price = command.Price,
        // Stock = command.Stock,
        // Category = command.Category,
        // ImagesUrls = command.ImagesUrls,
        // IsAvailable = true
        // };
        
        return await Task.FromResult(
            Result.Success(command.Name)
        );
    }
    
}