using Defender.UserManagement.Application.Models.Google;
using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Application.Common.Interfaces;

public interface IAuthService
{
    Task<User> Authenticate(GoogleUser googleUser);
}
