using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IList<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User updatedUser);
    Task RemoveUserAsync(Guid id);
}
