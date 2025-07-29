using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Commands.GoogleAuth;

public class GoogleAuthCommandHandler(IRepository repository, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenHasher refreshTokenHasher) : ICommandHandler<GoogleAuthCommand, GoogleAuthResponseDto>
{
    public async Task<Result<GoogleAuthResponseDto>> Handle(GoogleAuthCommand command, CancellationToken cancellationToken = default)
    {

        var user = await repository.Users.FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            user = new User
            {
                FullName = command.FullName,
                Email = command.Email,
                Provider = command.Provider,
                ProviderId = command.ProviderId,
                ProfilePicture = command.ProfilePicture,
                Phone = command.Phone,
                Role = command.Role
            };
                 
            repository.Users.Add(user);
            await repository.SaveChangesAsync(cancellationToken);
        }
        
        var accessToken = accessTokenGenerator.GenerateToken(user);
        var refreshToken = refreshTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenHashed = refreshTokenHasher.Hash(refreshToken);
        
        var refreshTokenCreate = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenHashed,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
        };
                                 
        repository.RefreshTokens.Add(refreshTokenCreate);
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new GoogleAuthResponseDto(accessToken, refreshToken)));
    }
}