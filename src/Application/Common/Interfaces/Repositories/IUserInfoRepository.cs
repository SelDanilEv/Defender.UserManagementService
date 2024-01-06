using Defender.Common.DB.Model;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces.Repositories;

public interface IUserInfoRepository
{
    Task<IList<UserInfo>> GetUserInfosByAllFieldsAsync(UserInfo account);
    Task<UserInfo> GetUserInfoByIdAsync(Guid userId);
    Task<UserInfo> GetUserInfoByLoginAsync(string login);
    Task<bool> CheckIfEmailTakenAsync(string email);
    Task<UserInfo> CreateUserInfoAsync(UserInfo account);
    Task<UserInfo> UpdateUserInfoAsync(UpdateModelRequest<UserInfo> request);
}
