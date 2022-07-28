using AutoMapper;
using Defender.UserManagement.Application.DTOs;
using Defender.UserManagement.Domain.Entities.User;

namespace Defender.UserManagement.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.Value.ToShortDateString()));
    }
}
