using EnviosYa.Application.Common;
using Microsoft.EntityFrameworkCore;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandHandler(IRepository repository) : ICommandHandler<CreateProductCommand, CreateProductResponseDto>
{
    public async Task<Result<CreateProductResponseDto>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        // var productExits = await repository.Products.AnyAsync(p => p.Name == command.Name && p.IsAvailable, cancellationToken);
        //
        // if (productExits)
        // {
        //     return await Task.FromResult(
        //         Result.Failure<CreateProductResponseDto>(Error.Conflict("400","Product already exists"))
        //     );
        // }
        
        // var product = new Domain.Entities.Product
        // {
        // Name = command.Name,
        // Price = command.Price,
        // Stock = command.Stock,
        // Category = command.Category,
        // ImagesUrls = command.ImagesUrls,
        // IsAvailable = true
        // };
        //
        // repository.Products.Add(product);
        // await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(
            Result.Success(new CreateProductResponseDto(command.Name))
        );
    }
}