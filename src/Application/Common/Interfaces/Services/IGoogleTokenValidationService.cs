using Defender.UserManagement.Application.Models.Google;

namespace Defender.UserManagement.Application.Common.Interfaces;

public interface IGoogleTokenValidationService
{
    Task<GoogleUser> GetTokenInfoAsync(string token);
}
