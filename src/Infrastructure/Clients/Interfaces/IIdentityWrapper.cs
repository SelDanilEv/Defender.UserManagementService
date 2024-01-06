using Defender.Common.Clients.Identity;

namespace Defender.UserManagementService.Infrastructure.Clients.Interfaces;

public interface IIdentityWrapper
{
    public Task<AccountDto> UpdateAccountVerificationAsync(Guid accountId, bool isEmailVerified);

}
