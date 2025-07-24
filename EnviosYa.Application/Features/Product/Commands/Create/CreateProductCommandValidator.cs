using System.Globalization;
using System.Text;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Domain.Constants;
using FluentValidation;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductCommandValidator()
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
            .Must(cat => TryParseCategory(cat, out var category)).WithMessage("Category must be a valid category.");
        
        RuleFor(x => x.ImagesUrls)
            .Must(urls => urls.Count < 5).WithMessage("ImagesUrls must contain at least 5 urls.")
            .ForEach(url => 
                url
                    .NotEmpty().WithMessage("ImageUrl is not empty.")
                    .Must(IsValidUrl).WithMessage("ImageUrl is not valid.")
                );
        
    }

    public static bool TryParseCategory(string Category, out CategoryProduct category)
    {
        string normalized = NormalizeString(Category);

        return Enum.TryParse(normalized, ignoreCase: true, out category);
    }

    private static string NormalizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        string normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        normalized = sb.ToString().Normalize(NormalizationForm.FormC);

        normalized = normalized.Replace(" ", "")
            .Replace("-", "")
            .Replace("_", "")
            .Trim();

        return normalized;
    }
    
    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}