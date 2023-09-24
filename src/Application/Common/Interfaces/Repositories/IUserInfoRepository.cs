using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces.Repositories;

public interface IUserInfoRepository
{
    Task<IList<UserInfo>> GetUserInfosByAllFieldsAsync(UserInfo account);
    Task<UserInfo> GetUserInfoByLoginAsync(string login);
    Task<UserInfo> CreateUserInfoAsync(UserInfo account);
}
