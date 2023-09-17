using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Infrastructure.Services;
public class UserManagementService : IUserManagementService
{
    private readonly IUserInfoRepository _userInfoRepository;

    public UserManagementService(
        IUserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }

    public async Task<UserInfo> CreateUserAsync(string email, string phoneNumber, string nickname)
    {
        var user = new UserInfo
        {
            Email = email,
            PhoneNumber = phoneNumber,
            Nickname = nickname
        };

        await CheckUserUniquenessAsync(user);

        return await CreateUserAsync(user);
    }

    public async Task<UserInfo> GetUsersByLoginAsync(string login)
    {
        return await _userInfoRepository.GetUserInfoByLoginAsync(login);
    }

    private async Task CheckUserUniquenessAsync(UserInfo user)
    {
        var users = await _userInfoRepository.GetUserInfosByAllFieldsAsync(user);

        users = users.Where(x => x.Id != user.Id).ToList();

        foreach (var existingUser in users)
        {
            if (existingUser.Email == user.Email)
            {
                throw new ServiceException(ErrorCode.BR_USM_EmailAddressInUse);
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && existingUser.PhoneNumber == user.PhoneNumber)
            {
                throw new ServiceException(ErrorCode.BR_USM_PhoneNumberInUse);
            }

            if (existingUser.Nickname == user.Nickname)
            {
                throw new ServiceException(ErrorCode.BR_USM_NicknameInUse);
            }
        }
    }

    private async Task<UserInfo> CreateUserAsync(UserInfo user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.Now;

        await _userInfoRepository.CreateUserInfoAsync(user);

        return user;
    }

    private async Task<UserInfo> UpdateUserAsync(UserInfo user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.Now;

        await _userInfoRepository.UpdateUserInfoAsync(user);

        return user;
    }
}
