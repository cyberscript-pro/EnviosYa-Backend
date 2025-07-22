namespace EnviosYa.Application.Common.Abstractions;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(
        TCommand command,
        CancellationToken cancellationToken
    );
}
public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<string>> Handle(TCommand command, CancellationToken cancellationToken = default);
}