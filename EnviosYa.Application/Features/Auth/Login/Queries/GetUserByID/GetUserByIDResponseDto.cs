namespace EnviosYa.Application.Features.Auth.Login.Queries.GetUserByID;

public record GetUserByIDResponseDto(
    string FullName,
    string Email,
    string? ProfilePicture,
    string? Phone,
    string? Cart
    );