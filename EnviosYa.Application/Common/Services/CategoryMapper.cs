using System.Globalization;
using System.Text;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Common.Services;

public class CategoryMapper
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
}