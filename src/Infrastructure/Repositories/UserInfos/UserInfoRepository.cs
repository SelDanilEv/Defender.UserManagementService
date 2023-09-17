using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Application.Configuration.Options;
using Defender.UserManagementService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Defender.UserManagementService.Infrastructure.Repositories.UserInfos;

public class UserInfoRepository : MongoRepository<UserInfo>, IUserInfoRepository
{
    public UserInfoRepository(IOptions<MongoDbOptions> mongoOption) : base(mongoOption.Value)
    {
    }

    #region Default methods

    public async Task<IList<UserInfo>> GetAllUserInfosAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<UserInfo> GetUserInfoByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }

    public async Task<UserInfo> CreateUserInfoAsync(UserInfo user)
    {
        return await AddItemAsync(user);
    }

    public async Task<UserInfo> UpdateUserInfoAsync(UserInfo updatedUserInfo)
    {
        return await UpdateItemAsync(updatedUserInfo);
    }

    public async Task RemoveUserInfoAsync(Guid id)
    {
        await RemoveItemAsync(id);
    }

    #endregion

    public async Task<IList<UserInfo>> GetUserInfosByAllFieldsAsync(UserInfo account)
    {
        var filter = MergeFilters(Enums.MongoFilterOperator.OR,
                                        CreateFilterDefinition(a => a.Email, account.Email),
                                        CreateFilterDefinition(a => a.Nickname, account.Nickname)
                                        );

        if (string.IsNullOrWhiteSpace(account.PhoneNumber))
        {
            filter = MergeFilters(Enums.MongoFilterOperator.OR,
                                        filter,
                                        CreateFilterDefinition(a => a.PhoneNumber, account.PhoneNumber));
        }

        return await GetItemsWithFilterAsync(filter);
    }

    public async Task<UserInfo> GetUserInfoByLoginAsync(string login)
    {
        var filter = MergeFilters(Enums.MongoFilterOperator.OR,
                                        CreateFilterDefinition(a => a.Email, login),
                                        CreateFilterDefinition(a => a.PhoneNumber, login)
                                        );

        var users = await GetItemsWithFilterAsync(filter);

        if (users.Count == 0)
            throw new NotFoundException(ErrorCode.BR_USM_UserWithSuchLoginIsNotExist);

        return users.FirstOrDefault();
    }
}
