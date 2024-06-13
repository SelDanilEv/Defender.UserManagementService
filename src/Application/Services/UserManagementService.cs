using Defender.Common.DB.Model;
using Defender.Common.DB.Pagination;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Application.Common.Interfaces.Wrappers;
using Defender.UserManagementService.Application.Models;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Services;
public class UserManagementService(
        IUserInfoRepository userInfoRepository,
        IIdentityWrapper identityWrapper)
    : IUserManagementService
{

    public async Task<PagedResult<UserInfo>> GetUsersAsync(PaginationRequest paginationRequest)
    {
        return await userInfoRepository.GetUsersAsync(paginationRequest);
    }

    public async Task<UserInfo> GetUserByIdAsync(Guid userId)
    {
        return await userInfoRepository.GetUserInfoByIdAsync(userId);
    }

    public async Task<UserInfo> GetUserByLoginAsync(string login)
    {
        return await userInfoRepository.GetUserInfoByLoginAsync(login);
    }

    public async Task<UserInfo> CreateUserAsync(string email, string phoneNumber, string nickname)
    {
        var user = new UserInfo
        {
            Email = email,
            PhoneNumber = phoneNumber,
            Nickname = nickname
        };

        await ThrowIfUserExistsAsync(user);

        return await CreateUserAsync(user);
    }

    public async Task<UserInfo> UpdateUserAsync(UpdateUserInfoRequest request)
    {
        await ThrowIfUserExistsAsync(request.ToUserInfo());

        var updateRequest = CreateUpdateRequest(request);

        var updatedUser = await userInfoRepository.UpdateUserInfoAsync(updateRequest);

        if (!string.IsNullOrEmpty(request.Email))
        {
            await identityWrapper.UpdateAccountVerificationAsync(
                request.Id,
                false);
        }

        return updatedUser;
    }

    public async Task<bool> CheckIfEmailTakenAsync(string login)
    {
        return await userInfoRepository.CheckIfEmailTakenAsync(login);
    }

    private async Task ThrowIfUserExistsAsync(UserInfo user)
    {
        var users = await userInfoRepository.GetUsersInfoByAllFieldsAsync(user);

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
    }

    private async Task<UserInfo> CreateUserAsync(UserInfo user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.Now;

        await userInfoRepository.CreateUserInfoAsync(user);

        return user;
    }

    public UpdateModelRequest<UserInfo> CreateUpdateRequest(UpdateUserInfoRequest request)
    {
        var updateRequest = UpdateModelRequest<UserInfo>.Init(request.Id)
            .SetIfNotNull(x => x.Email, request.Email)
            .SetIfNotNull(x => x.PhoneNumber, request.PhoneNumber)
            .SetIfNotNull(x => x.Nickname, request.Nickname);

        return updateRequest;
    }
}
