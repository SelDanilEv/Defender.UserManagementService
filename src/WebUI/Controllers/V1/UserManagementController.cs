using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.UserManagement.WebUI.Attributes;
using Defender.UserManagement.Application.Modules.Users.Queries;
using Defender.UserManagement.Application.Modules.Users.Commands;
using Defender.UserManagement.Domain.Models;
using Defender.UserManagement.Application.DTOs;

namespace Defender.UserManagement.WebUI.Controllers.V1;

public class UserManagementController : BaseApiController
{

    public UserManagementController(
        IMediator mediator,
        IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("admin/users")]
    [Auth(Roles.Admin, Roles.SuperAdmin)]
    [ProducesResponseType(typeof(IList<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IList<UserDto>> GetAllUsersAsync([FromQuery] GetAllUsersQuery getAllUsersQuery)
    {
        return await ProcessApiCallAsync<GetAllUsersQuery, IList<UserDto>>(getAllUsersQuery);
    }

    [HttpPut("admin/user")]
    [Auth(Roles.Admin, Roles.SuperAdmin)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> UpdateUserFromAdminAsync([FromBody] UpdateAccountFromAdminCommand updateUserCommand)
    {
        return await ProcessApiCallAsync<UpdateAccountFromAdminCommand, UserDto>(updateUserCommand);
    }

    [HttpPut("user/user")]
    [Auth(Roles.User)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> UpdateUserFromUserAsync([FromBody] UpdateAccountFromUserCommand updateUserCommand)
    {
        return await ProcessApiCallAsync<UpdateAccountFromUserCommand, UserDto>(updateUserCommand);
    }

    [HttpDelete("admin/user")]
    [Auth(Roles.SuperAdmin)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task RemoveUserAsync([FromQuery] RemoveUserCommand updateUserCommand)
    {
        await ProcessApiCallAsync(updateUserCommand);
    }
}
