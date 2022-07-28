using MongoDB.Bson.Serialization.Attributes;

namespace Defender.UserManagement.Domain.Entities.User;

public class User : IBaseModel
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedDate { get; set; }
    public List<string> Roles { get; set; } = new List<string>();


    [BsonIgnore]
    public bool IsSuperAdmin => Roles.Contains(Models.Roles.SuperAdmin);

    public bool HasRole(string role) => Roles.Contains(role);
    public string GetHighestRole()
    {
        if (Roles.Contains(Models.Roles.SuperAdmin)) return Models.Roles.SuperAdmin;
        if (Roles.Contains(Models.Roles.Admin)) return Models.Roles.Admin;
        if (Roles.Contains(Models.Roles.User)) return Models.Roles.User;

        return String.Empty;
    }

}
