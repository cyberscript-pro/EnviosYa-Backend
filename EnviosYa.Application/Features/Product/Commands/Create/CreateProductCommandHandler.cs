using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, string>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<string>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        // var product = new Domain.Entities.Product
        // {
        //     Name = command.Name,
        //     Description = command.Description,
        //     Price = command.Price,
        //     Stock = command.Stock,
        //     Category = command.Category,
        //     ImagesUrls = command.ImagesUrls,
        //     IsAvailable = true
        // };
        //
        // await _productRepository.AddAsync(product, cancellationToken);
        //
        // return product.Id;
        
        return await Task.FromResult(
            Result.Success($"Hello Product Handler")
        );
    }
    
}