using System.Globalization;
using System.Text;
using EnviosYa.Domain.Constants;

namespace EnviosYa.Application.Common.Services;

public class ProviderMapper
{
    public static bool TryParseProvider(string provider, out Provider Provider)
    {
        string normalized = NormalizeString(provider);

        return Enum.TryParse(normalized, ignoreCase: true, out Provider);
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