namespace EnviosYa.Application.Common.Abstractions;

public interface ICommandDispatcher
{
    Task<TResponse> DispatchAsync<TResponse>(ICommand<TResponse> command,
        CancellationToken cancellationToken = default);
}