using AutoMapper;
using Defender.Common.Clients.Identity;
using Defender.Common.Interfaces;
using Defender.Common.Wrapper.Internal;
using Defender.UserManagementService.Infrastructure.Clients.Interfaces;

namespace Defender.UserManagementService.Infrastructure.Clients.Identity;

public class IdentityWrapper : BaseInternalSwaggerWrapper, IIdentityWrapper
{
    private readonly IMapper _mapper;
    private readonly IIdentityServiceClient _identityServiceClient;

    public IdentityWrapper(
        IAuthenticationHeaderAccessor authenticationHeaderAccessor,
        IIdentityServiceClient identityClient,
        IMapper mapper) : base(
            identityClient,
            authenticationHeaderAccessor)
    {
        _identityServiceClient = identityClient;
        _mapper = mapper;
    }

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

            var response = await _identityServiceClient.UpdateAsync(updateCommand);

            return response;
        }, AuthorizationType.Service);
    }

}
