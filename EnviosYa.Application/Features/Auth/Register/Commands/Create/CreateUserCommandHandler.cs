using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Cart.Commands.Create;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Register.Commands.Create;

public class CreateUserCommandHandler(IRepository repository, IPasswordHasher hasher) : ICommandHandler<CreateUserCommand, CreateUserResponseDto>
{
    public async Task<Result<CreateUserResponseDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {

        if (await repository.Users.AnyAsync(u => u.Nickname == command.Nickname || u.Email == command.Email,
                cancellationToken))
        {
            return await Task.FromResult(Result.Failure<CreateUserResponseDto>(Error.Conflict("400", "Email or Nickname already exists")));
        }
        
        var user = new User
        {
            FullName = command.FullName,
            Email = command.Email,
            Nickname = command.Nickname,
            Role = command.Role,
            Password = hasher.Hash(command.Password),
            ProfilePicture = command.ProfilePicture,
            Phone = command.Phone
        };

        repository.Users.Add(user);
        await repository.SaveChangesAsync(cancellationToken);

        var commandCart = new CreateCardCommand
        {
            UserId = user.Id,
            User = user
        };
        var handler = new CreateCardCommandHandler(repository);
        
        var result = await handler.Handle(commandCart);

        return await Task.FromResult(Result.Success(new CreateUserResponseDto(
            command.Nickname,
            command.Email,
            result.Value.Id
        )));
    }
}