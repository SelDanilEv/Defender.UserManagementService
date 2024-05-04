using Defender.Common.Clients.Identity;
using Defender.Common.Interfaces;
using Defender.Common.Wrapper.Internal;
using Defender.UserManagementService.Infrastructure.Clients.Interfaces;

namespace Defender.UserManagementService.Infrastructure.Clients.Identity;

public class IdentityWrapper(
        IAuthenticationHeaderAccessor authenticationHeaderAccessor,
        IIdentityServiceClient identityClient)
    : BaseInternalSwaggerWrapper(
            identityClient,
            authenticationHeaderAccessor),
    IIdentityWrapper
{
    public async Task<AccountDto> UpdateAccountVerificationAsync(
        Guid accountId,
        bool isEmailVerified)
    {
        return await ExecuteSafelyAsync(async () =>
        {
            var updateCommand = new UpdateAccountCommand
            {
                Id = accountId,
                IsEmailVerified = isEmailVerified
            };

            var response = await identityClient.UpdateAsync(updateCommand);

            return response;
        }, AuthorizationType.Service);
    }

    public async Task<bool> VerifyUpdateUserAccessCodeAsync(int code)
    {
        return await ExecuteSafelyAsync(async () =>
        {
            var command = new VerifyCodeCommand()
            {
                Code = code,
                Type = VerifyCodeCommandType.UpdateAccount
            };

            var response = await identityClient.VerifyAsync(command);

            return response;
        }, AuthorizationType.User);
    }

}
