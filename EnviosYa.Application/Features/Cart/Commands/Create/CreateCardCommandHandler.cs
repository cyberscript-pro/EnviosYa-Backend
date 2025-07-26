using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.Cart.Commands.Create;

public class CreateCardCommandHandler(IRepository repository) : ICommandHandler<CreateCardCommand, CreateCartResponseDto>
{
    public async Task<Result<CreateCartResponseDto>> Handle(CreateCardCommand command, CancellationToken cancellationToken = default)
    {
        var cart = new Domain.Entities.Cart
        {
            UserId = command.UserId,
            User = command.User,
            Items =  new List<Domain.Entities.CartItem>()
        };

        repository.Carts.Add(cart);
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new CreateCartResponseDto(cart.Id.ToString())));
    }
}