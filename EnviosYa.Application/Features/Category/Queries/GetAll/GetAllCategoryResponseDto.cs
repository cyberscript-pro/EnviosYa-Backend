namespace EnviosYa.Application.Features.Category.Queries.GetAll;

public record GetAllCategoryResponseDto(
    IEnumerable<Translation> Translations,
    string Id
    );

public record Translation(
    string Name,
    string Language
    );