namespace Defender.UserManagementService.Application.Common.Interfaces.Services;

public interface IAccessCodeService
{
    Task<bool> VerifyUpdateUserAccessCodeAsync(int code);
}
