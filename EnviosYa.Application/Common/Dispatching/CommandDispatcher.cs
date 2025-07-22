using EnviosYa.Application.Common.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnviosYa.Application.Common.Dispatching;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task<TResponse> DispatchAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var validatorType = typeof(IValidator).MakeGenericType(command.GetType());
        var validator = _serviceProvider.GetService(validatorType) as IValidator;

        if (validator != null)
        {
            var context = new ValidationContext<object>(command);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                var errors = string.Join(", ",  result.Errors.Select(x => x.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
        
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        
        return await handler.HandleAsync((dynamic)command, cancellationToken);
    }
}