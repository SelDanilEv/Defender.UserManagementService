using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces;

public interface IUserManagementService
{
    Task<UserInfo> GetUsersByIdAsync(Guid userId);
    Task<UserInfo> GetUsersByLoginAsync(string login);
    Task<bool> CheckIfEmailTakenAsync(string email);
    Task<UserInfo> CreateUserAsync(string email, string phoneNumber, string nickname);

}
