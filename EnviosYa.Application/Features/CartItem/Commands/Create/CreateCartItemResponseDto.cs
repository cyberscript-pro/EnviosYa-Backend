namespace EnviosYa.Application.Features.CartItem.Commands.Create;

public record CreateCartItemResponseDto(string ProductId, int Quantity, string CartId);