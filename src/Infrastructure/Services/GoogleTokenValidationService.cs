using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Application.Models.Google;
using Defender.UserManagement.Infrastructure.Clients.Interfaces;

namespace Defender.UserManagement.Infrastructure.Services;
public class GoogleTokenValidationService : IGoogleTokenValidationService
{
    private readonly IGoogleClient _googleClient;

    public GoogleTokenValidationService(IGoogleClient googleClient)
    {
        _googleClient = googleClient;
    }

    public async Task<GoogleUser> GetTokenInfoAsync(string token)
    {
        if (token == null)
            throw new ArgumentNullException(nameof(token));

        return await _googleClient.GetTokenInfo(token);
    }
}
