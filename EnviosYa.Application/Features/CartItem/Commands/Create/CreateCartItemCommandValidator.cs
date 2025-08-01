using EnviosYa.Application.Features.CartItem.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemDto>
{
    public readonly IRepository Repository;
    public CreateCartItemCommandValidator(IRepository repository)
    {
        Repository = repository;
        
        RuleFor(ci => ci.ProductoId)
            .NotEmpty().WithMessage("ProductoId is required.")
            .MustAsync(async (productoId, cancellation) =>
            {
                var product = await Repository.Products.FirstOrDefaultAsync(ci => ci.Id == Guid.Parse(productoId), cancellationToken: cancellation);
                return product is not null;
            }).WithMessage("Product not found.");
        
        RuleFor(ci => ci.Cantidad)
            .NotEmpty().WithMessage("Cantidad is required.");
    }
}