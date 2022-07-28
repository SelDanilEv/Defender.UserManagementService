using Microsoft.AspNetCore.Authorization;

namespace Defender.UserManagement.WebUI_old.Attributes;

public class AuthAttribute : AuthorizeAttribute
{
    public AuthAttribute(params string[] roles)
    {
        Roles = String.Join(",", roles);
    }
}

