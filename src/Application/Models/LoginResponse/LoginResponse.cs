using Defender.UserManagement.Application.DTOs;

namespace Defender.UserManagement.Application.Models.LoginResponse;
public class LoginResponse
{
    public bool Authorized { get; set; } = false;

    public string? Token { get; set; }

    public UserDto? UserDetails { get; set; }

}
