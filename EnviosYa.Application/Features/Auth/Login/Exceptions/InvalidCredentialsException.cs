using ApplicationException = EnviosYa.Application.Common.Exceptions.ApplicationException;

namespace EnviosYa.Application.Features.Auth.Login.Exceptions;

public sealed class InvalidCredentialsException(string message): ApplicationException(message)
{
    public override string ErrorCode => "InvalidCredentialsException";
}