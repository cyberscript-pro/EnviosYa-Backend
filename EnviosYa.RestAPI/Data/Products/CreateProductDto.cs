using System.Globalization;
using System.Text;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Domain.Constants;

namespace EnviosYa.RestAPI.Data.Products;

public record CreateProductDto( 
    string Name,
    string Description,
    double Price,
    int Stock,
    string Category,
    List<string> ImagesUrls
);

public static class CreateProductDtoToCommand
{
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
    public static CreateProductCommand ToCommand(this CreateProductDto dto)
    {
        TryParseCategory(dto.Category, out var category);
        
        return new CreateProductCommand
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = category,
            ImagesUrls = dto.ImagesUrls
        };
    }
}