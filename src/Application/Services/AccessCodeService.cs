using Defender.UserManagementService.Application.Common.Interfaces.Services;
using Defender.UserManagementService.Application.Common.Interfaces.Wrappers;

namespace Defender.UserManagementService.Application.Services;

public class AccessCodeService(
        IIdentityWrapper identityWrapper)
    : IAccessCodeService
{
    public async Task<bool> VerifyUpdateUserAccessCodeAsync(int code)
    {
        return await identityWrapper.VerifyUpdateUserAccessCodeAsync(code);
    }
}
