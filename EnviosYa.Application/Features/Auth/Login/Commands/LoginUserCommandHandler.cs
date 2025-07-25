using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Login.Exceptions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Commands;

public class LoginUserCommandHandler(IRepository repository, ITokenGenerator tokenGenerator, IPasswordHasher hasher) : ICommandHandler<LoginUserCommand, LoginUserResponseDto>
{
    public async Task<Result<LoginUserResponseDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken = default)
    {

        var user = await repository.Users.FirstOrDefaultAsync(u => u.Email == command.Email && u.IsAvailable,
            cancellationToken);

        if (user is null)
        {
            return await Task.FromResult(Result.Failure<LoginUserResponseDto>(Error.NotFound("400","User not found")));
            //throw new UserNotFoundException("User not found");
        }

        if (!hasher.Verify(command.Password, user.Password))
        {
            return await Task.FromResult(Result.Failure<LoginUserResponseDto>(Error.Failure("409", "Invalid password")));
            //throw new InvalidCredentialsException("Invalid credentials");
        }

        var token = tokenGenerator.GenerateToken(user);
        
        return new LoginUserResponseDto(token);
    }
}