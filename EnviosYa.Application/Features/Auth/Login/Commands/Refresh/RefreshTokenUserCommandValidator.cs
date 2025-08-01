using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Login.DTOs;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Commands.Refresh;

public class RefreshTokenUserCommandValidator : AbstractValidator<RefreshTokenDto>
{
    private readonly IRepository Repository;
    private readonly IRefreshTokenHasher Hasher;
    
    public RefreshTokenUserCommandValidator(IRepository repository,  IRefreshTokenHasher hasher)
    {
        Repository = repository;
        Hasher = hasher;
        
        RuleFor(x => x.RefreshToken)
            .NotNull().NotEmpty().WithMessage("RefreshToken required")
            .MustAsync(async (refreshToken, cancellationToken) =>
            {
                var refreshTokenExists = await Repository.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == Hasher.Hash(refreshToken), cancellationToken);

                return refreshTokenExists is not null && !refreshTokenExists.RevokedAt.HasValue &&
                       (refreshTokenExists.CreatedAt < refreshTokenExists.ExpiresAt);
            }).WithMessage("RefreshToken not accepted");
    }
}