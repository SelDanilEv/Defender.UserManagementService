namespace Defender.UserManagement.Application.Common.Exceptions;

public class GoogleClientException : Exception
{
    public GoogleClientException()
        : base("Something goes wrong during a request to google APIs.")
    {
    }
}
