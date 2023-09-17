using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces.Repositories;

public interface IUserInfoRepository
{
    Task<IList<UserInfo>> GetAllUserInfosAsync();
    Task<IList<UserInfo>> GetUserInfosByAllFieldsAsync(UserInfo account);
    Task<UserInfo> GetUserInfoByIdAsync(Guid id);
    Task<UserInfo> GetUserInfoByLoginAsync(string login);
    Task<UserInfo> CreateUserInfoAsync(UserInfo account);
    Task<UserInfo> UpdateUserInfoAsync(UserInfo updatedAccount);
    Task RemoveUserInfoAsync(Guid id);
}
