namespace EnviosYa.Application.Common.Abstractions;

public interface IBaseCommand;
public interface ICommand: IBaseCommand;
public interface ICommand<TResponse> : IBaseCommand;