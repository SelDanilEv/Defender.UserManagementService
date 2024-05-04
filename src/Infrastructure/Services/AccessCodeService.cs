using Defender.UserManagementService.Application.Common.Interfaces.Services;
using Defender.UserManagementService.Infrastructure.Clients.Interfaces;

namespace Defender.UserManagementService.Infrastructure.Services;

public class AccessCodeService(
        IIdentityWrapper identityWrapper)
    : IAccessCodeService
{
    public async Task<bool> VerifyUpdateUserAccessCodeAsync(int code)
    {
        return await identityWrapper.VerifyUpdateUserAccessCodeAsync(code);
    }
}
