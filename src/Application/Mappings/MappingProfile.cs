using AutoMapper;
using Defender.Common.DTOs;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserInfo, UserDto>();
    }
}
