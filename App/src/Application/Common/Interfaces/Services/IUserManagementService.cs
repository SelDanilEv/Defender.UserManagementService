using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Application.Common.Interfaces;

public interface IUserManagementService
{
    Task<IList<User>> GetAllUsersAsync();

    Task<User> GetUserByIdAsync(Guid id);

    Task<User> GetUsersByEmailAsync(string email);

    Task<User> UpdateUserAsync(User user);

    Task RemoveUserAsync(Guid id);
}
