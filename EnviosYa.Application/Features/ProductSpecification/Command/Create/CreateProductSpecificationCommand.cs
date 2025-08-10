using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.ProductSpecification.Command.Create;

public class CreateProductSpecificationCommand : ICommand<CreateProductSpecificationResponseDto>
{
    public required Guid ProductId { get; init; }
    
    public ICollection<Domain.Entities.ProductSpecificationTranslations> Translations { get; init; } =  new List<Domain.Entities.ProductSpecificationTranslations>();
}