using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Login.Queries.GetUserByID;

public class GetUserByIDQueryHandler(IRepository repository) : IQueryHandler<GetUserByIDQuery, GetUserByIDResponseDto>
{
    public async Task<Result<GetUserByIDResponseDto>> Handle(GetUserByIDQuery query, CancellationToken cancellationToken = default)
    {
        var user = await repository.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == query.Id, cancellationToken);

        if (user is null)
        {
            return await Task.FromResult(Result.Failure<GetUserByIDResponseDto>(Error.NotFound("400", "User not found")));
        }

        return await Task.FromResult(Result.Success(new GetUserByIDResponseDto(
            FullName: user.FullName,
            Nickname: user.Nickname,
            Email: user.Email,
            ProfilePicture: user.ProfilePicture,
            Phone: user.Phone,
            Cart: user.Cart?.Id.ToString()
        )));
    }
}