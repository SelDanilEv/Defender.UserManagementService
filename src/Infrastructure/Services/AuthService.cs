using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Application.Common.Interfaces.Repositories.Users;
using Defender.UserManagement.Application.Models;
using Defender.UserManagement.Application.Models.Google;
using Defender.UserManagement.Domain.Entities.User;
using Defender.UserManagement.Domain.Models;

namespace Defender.UserManagement.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Authenticate(GoogleUser googleUser)
    {
        if (googleUser == null)
        {
            throw new ArgumentNullException(nameof(googleUser));
        }

        return await FindUserOrAdd(googleUser);
    }

    private async Task<User> FindUserOrAdd(GoogleUser googleUser)
    {
        var user = await _userRepository.GetUserByEmailAsync(googleUser.Email);

        if (user == null)
        {
            user = new User()
            {
                Email = googleUser.Email,
                Name = googleUser.Name,
                CreatedDate = DateTime.Now,
                Roles = new List<string> { Roles.User }
            };

            await _userRepository.CreateUserAsync(user);
        }

        return user;
    }
}
