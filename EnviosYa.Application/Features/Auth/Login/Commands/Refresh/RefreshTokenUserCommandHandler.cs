using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Commands.Refresh;

public class RefreshTokenUserCommandHandler(IRepository repository, IRefreshTokenHasher hasher, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator) : ICommandHandler<RefreshTokenUserCommand, RefreshTokenUserResponseDto>
{
    public async Task<Result<RefreshTokenUserResponseDto>> Handle(RefreshTokenUserCommand command, CancellationToken cancellationToken = default)
    {
        var refreshTokenExists = await repository.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == hasher.Hash(command.RefreshToken), cancellationToken);

        if (refreshTokenExists is null || refreshTokenExists.RevokedAt.HasValue ||
            (refreshTokenExists.CreatedAt >= refreshTokenExists.ExpiresAt))
        {
            return await Task.FromResult(Result.Failure<RefreshTokenUserResponseDto>(Error.Conflict("401", "RefreshToken not accepted")));
        }

        var accessToken = accessTokenGenerator.GenerateToken(refreshTokenExists.User);
        var refreshToken = command.RefreshToken;
        
        var tiempoRestante = refreshTokenExists.ExpiresAt - DateTime.UtcNow;

        if (!(tiempoRestante.TotalDays < 2))
            return await Task.FromResult(Result.Success(new RefreshTokenUserResponseDto(accessToken, refreshToken)));
        
        refreshToken = refreshTokenGenerator.GenerateRefreshToken();
        refreshTokenExists.RevokedAt = DateTime.UtcNow;

        var refreshTokenHasher = hasher.Hash(refreshToken);
        
        var refreshTokenCreate = new RefreshToken
        {
            UserId = refreshTokenExists.User.Id,
            Token = refreshTokenHasher,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
        };
                                 
        repository.RefreshTokens.Add(refreshTokenCreate);
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new RefreshTokenUserResponseDto(accessToken, refreshToken)));
    }
}