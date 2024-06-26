﻿using Defender.Common.Clients.Identity;

namespace Defender.UserManagementService.Application.Common.Interfaces.Wrappers;

public interface IIdentityWrapper
{
    public Task<AccountDto> UpdateAccountVerificationAsync(Guid accountId, bool isEmailVerified);

    Task<bool> VerifyUpdateUserAccessCodeAsync(int code);
}
