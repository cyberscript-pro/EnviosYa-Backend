using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Commands.Login;

public class LoginUserCommandHandler(IRepository repository, IAccessTokenGenerator accessTokenGenerator, IPasswordHasher hasher, IRefreshTokenHasher refreshTokenHasher, IRefreshTokenGenerator refreshTokenGenerator) : ICommandHandler<LoginUserCommand, LoginUserResponseDto>
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

        if (user.Provider == Provider.Credentials && !hasher.Verify(command.Password, user.Password))
        {
            return await Task.FromResult(Result.Failure<LoginUserResponseDto>(Error.Failure("409", "Invalid password")));
            //throw new InvalidCredentialsException("Invalid credentials");
        }

        var accessToken = accessTokenGenerator.GenerateToken(user);

        var refreshToken = refreshTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenHash = refreshTokenHasher.Hash(refreshToken);

        var refreshTokenCreate = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),

        };
        
        repository.RefreshTokens.Add(refreshTokenCreate);
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success(new LoginUserResponseDto(accessToken, refreshToken)));
    }
}