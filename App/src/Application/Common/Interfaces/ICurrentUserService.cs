using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Application.Common.Interfaces;

public interface ICurrentUserService
{
    User? User { get; }
}
