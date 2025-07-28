using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Auth.Login.Queries.GetUserByID;

public class GetUserByIDQuery : IQuery<GetUserByIDResponseDto>
{
    public Guid Id { get; set; }
}