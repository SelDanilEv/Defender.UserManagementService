namespace Defender.UserManagement.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }

    public ForbiddenAccessException(string message)
    : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ForbiddenAccessException(string action, string role)
        : base($"Action \"{action}\" is forbidden for role \"{role}\"")
    {
    }
}
