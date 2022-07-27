using Defender.UserManagement.Application.Models.Google;

namespace Defender.UserManagement.Infrastructure.Clients.Interfaces;
public interface IGoogleClient
{
    Task<GoogleUser> GetTokenInfo(string token);
}
