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
        
        RuleFor(ci => ci.CartId)
            .NotEmpty().WithMessage("CartId is required.")
            .MustAsync(async (cartId, cancellation) =>
            {
                var cart = await Repository.Carts.FirstOrDefaultAsync(ci => ci.Id == Guid.Parse(cartId), cancellationToken: cancellation);
                return cart is null;
            }).WithMessage("Cart not found.");
        
        RuleFor(ci => ci.ProductoId)
            .NotEmpty().WithMessage("ProductoId is required.")
            .MustAsync(async (productoId, cancellation) =>
            {
                var product = await Repository.Products.FirstOrDefaultAsync(ci => ci.Id == Guid.Parse(productoId), cancellationToken: cancellation);
                return product is null;
            }).WithMessage("Product not found.");
        
        RuleFor(ci => ci.Cantidad)
            .NotEmpty().WithMessage("Cantidad is required.");
        
        RuleFor(ci => ci.Product)
            .NotNull().WithMessage("Product is required.")
            .MustAsync(async (cart, product, cancellation) =>
            {
                var productExist = cart.ProductoId == product.Id.ToString();
                return !productExist;
            }).WithMessage("Product not found.");
        
        RuleFor(ci => ci.Cart)
            .NotNull().WithMessage("Cart is required.")
            .MustAsync(async (cartItem, cart, cancellation) =>
            {
                var cartExist = cartItem.CartId == cart.Id.ToString();
                return !cartExist;
            }).WithMessage("Cart not found.");
    }
}