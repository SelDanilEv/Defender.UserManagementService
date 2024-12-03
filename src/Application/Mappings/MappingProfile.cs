using Defender.Common.DTOs;
using Defender.Common.Mapping;
using Defender.UserManagementService.Application.DTOs;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<UserInfo, UserDto>();
        CreateMap<UserInfo, PublicUserInfoDto>();
    }
}
