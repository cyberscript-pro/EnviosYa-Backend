using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Language.Commands.Create;

public class CreateLanguageCommandHandler(IRepository repository) : ICommandHandler<CreateLanguageCommand, CreateLanguageResponseDto>
{
    public async Task<Result<CreateLanguageResponseDto>> Handle(CreateLanguageCommand command, CancellationToken cancellationToken = default)
    {
        var languageExists = await repository.Languages.FirstOrDefaultAsync(l => l.Code == command.Code, cancellationToken);

        if (languageExists is not null)
        {
            return await Task.FromResult(Result.Failure<CreateLanguageResponseDto>(Error.Conflict("400", "Language already exists")));
        }

        var language = new Domain.Entities.Language
        {
            Code = command.Code,
            Name = command.Name
        };
        
        repository.Languages.Add(language);
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new CreateLanguageResponseDto(language.Id.ToString(), language.Code, language.Name)));
    }
}