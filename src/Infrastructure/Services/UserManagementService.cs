using Defender.UserManagement.Application.Common.Exceptions;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Application.Common.Interfaces.Repositories.Users;
using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Infrastructure.Services;
public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepository;

    public UserManagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<User>> GetAllUsersAsync()
    {
        var allUsers = await _userRepository.GetAllUsersAsync();

        //allUsers = allUsers.Select(x =>
        //{
        //    x.CreatedDate = x.CreatedDate.Value.;
        //    return x;
        //}).ToList();

       return allUsers;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> GetUsersByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id);

        if(oldUser.Email != user.Email)
        {
            var userWithNewEmail = await _userRepository.GetUserByEmailAsync(user.Email);

            if(userWithNewEmail != null)
            {
                throw new ValidationException("User with this email is already exist");
            }
        }

        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task RemoveUserAsync(Guid id)
    {
        await _userRepository.RemoveUserAsync(id);
    }
}
