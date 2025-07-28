namespace EnviosYa.Application.Features.Auth.Login.Queries.GetUserByID;

public record GetUserByIDResponseDto(
    string FullName,
    string Nickname,
    string Email,
    string? ProfilePicture,
    string? Phone,
    string? Cart
    );