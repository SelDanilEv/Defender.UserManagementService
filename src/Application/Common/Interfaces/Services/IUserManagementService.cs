using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces;

public interface IUserManagementService
{
    Task<UserInfo> GetUsersByGuidAsync(Guid userId);
    Task<UserInfo> GetUsersByLoginAsync(string login);
    Task<UserInfo> CreateUserAsync(string email, string phoneNumber, string nickname);

}
