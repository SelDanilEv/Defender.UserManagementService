using Defender.Common.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Defender.UserManagementService.Domain.Entities;

public class UserInfo : IBaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Nickname { get; set; }

    public DateTime? CreatedDate { get; set; }
}