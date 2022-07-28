namespace Defender.UserManagement.Application.DTOs;

public class UserDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? CreatedDate { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
