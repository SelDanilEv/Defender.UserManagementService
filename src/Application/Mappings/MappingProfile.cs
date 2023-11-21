using Defender.Common.DTOs;
using Defender.Common.Mapping;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<UserInfo, UserDto>();
    }
}
