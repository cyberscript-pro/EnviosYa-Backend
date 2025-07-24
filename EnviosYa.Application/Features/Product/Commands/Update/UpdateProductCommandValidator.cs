using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Product.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
        
        RuleFor(x => x.Category)
            .Must(cat => CategoryMapper.TryParseCategory(cat, out var category)).WithMessage("Category must be a valid category.");
        
        RuleFor(x => x.ImagesUrls)
            .Must(urls => urls.Count < 5).WithMessage("ImagesUrls must contain at least 5 urls.")
            .ForEach(url => 
                url
                    .NotEmpty().WithMessage("ImageUrl is not empty.")
                    .Must(IsValidUrl).WithMessage("ImageUrl is not valid.")
            );
        
    }
    
    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}