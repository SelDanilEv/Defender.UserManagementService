using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Models;

public class UpdateUserInfoRequest
{
    public UpdateUserInfoRequest AsUser()
    {
        Email = PhoneNumber = null;

        return this;
    }

    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Nickname { get; set; }

    public UserInfo ToUserInfo()
    {
        return new UserInfo
        {
            Id = Id,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Nickname = Nickname,
        };
    }
};

