using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductCommandValidator(IRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        RuleFor(x => x.DiscountPrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
        
        RuleFor(x => x.CategoryId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var category = await repository.Categories.FirstOrDefaultAsync(ca => ca.Id == Guid.Parse(id), cancellationToken);

                return category is not null;
            })
            .WithMessage("Category does not exist.");
        
        RuleFor(x => x.LanguageId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var language = await repository.Languages.FirstOrDefaultAsync(l => l.Id == Guid.Parse(id), cancellationToken);

                return language is not null;
            })
            .WithMessage("Language does not exist.");
        
        RuleFor(x => x.ProductImages)
            .Must(urls => urls.Count < 5).WithMessage("ProductImages must contain at least 5 urls.")
            .ForEach(url => 
                url
                    .NotEmpty().WithMessage("ProductImage is not empty.")
                    .Must(IsValidUrl).WithMessage("ProductImage is not valid.")
                );
        
    }
    
    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}