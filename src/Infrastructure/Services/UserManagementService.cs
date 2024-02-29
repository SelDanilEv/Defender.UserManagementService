using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Application.Modules.Users.Commands;
using Defender.UserManagementService.Domain.Entities;
using Defender.UserManagementService.Infrastructure.Clients.Interfaces;

namespace Defender.UserManagementService.Infrastructure.Services;
public class UserManagementService : IUserManagementService
{
    private readonly ICurrentAccountAccessor _accountAccessor;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IIdentityWrapper _identityWrapper;

    public UserManagementService(
        ICurrentAccountAccessor accountAccessor,
        IUserInfoRepository userInfoRepository,
        IIdentityWrapper identityWrapper)
    {
        _accountAccessor = accountAccessor;
        _userInfoRepository = userInfoRepository;
        _identityWrapper = identityWrapper;
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

    public async Task<UserInfo> UpdateUserAsync(UpdateUserCommand updateUserInfoCommand)
    {
        await CheckUserUniquenessAsync(updateUserInfoCommand.ToUserInfo());

        var updateRequest = updateUserInfoCommand.CreateUpdateRequest();

        var updatedUser = await _userInfoRepository.UpdateUserInfoAsync(updateRequest);

        if (!string.IsNullOrEmpty(updateUserInfoCommand.Email))
        {
            await _identityWrapper.UpdateAccountVerificationAsync(
                updateUserInfoCommand.UserId,
                false);
        }

        return updatedUser;
    }

    public async Task<UserInfo> GetUsersByIdAsync(Guid userId)
    {
        return await _userInfoRepository.GetUserInfoByIdAsync(userId);
    }

    public async Task<UserInfo> GetUsersByLoginAsync(string login)
    {
        return await _userInfoRepository.GetUserInfoByLoginAsync(login);
    }

    public async Task<bool> CheckIfEmailTakenAsync(string login)
    {
        return await _userInfoRepository.CheckIfEmailTakenAsync(login);
    }

    private async Task<UserInfo> CheckUserUniquenessAsync(UserInfo user)
    {
        var users = await _userInfoRepository.GetUserInfosByAllFieldsAsync(user);

        var thisUser = users.FirstOrDefault(x => x.Id == user.Id);

        if (thisUser != null)
            users.Remove(thisUser);

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

        return thisUser;
    }

    private async Task<UserInfo> CreateUserAsync(UserInfo user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.Now;

        await _userInfoRepository.CreateUserInfoAsync(user);

        return user;
    }
}
