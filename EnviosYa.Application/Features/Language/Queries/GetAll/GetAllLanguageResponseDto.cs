namespace EnviosYa.Application.Features.Language.Queries.GetAll;

public record GetAllLanguageResponseDto(
    string Id,
    string Code,
    string Name,
    IEnumerable<Translation>  Translations
    );


public record Translation(string Name);