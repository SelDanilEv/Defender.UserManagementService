using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.Common.Attributes;
using Defender.Common.Consts;
using Defender.Common.DTOs;
using Defender.UserManagementService.Application.Modules.Users.Commands;
using Defender.UserManagementService.Application.Modules.Users.Queries;
using Defender.UserManagementService.Application.DTOs;
using Defender.Common.DB.Pagination;
using System;

namespace Defender.UserManagementService.WebApi.Controllers.V1;

public class UserController(
        IMediator mediator,
        IMapper mapper)
    : BaseApiController(
        mediator,
        mapper)
{

    [Auth(Roles.Admin)]
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> GetByIdAsync(
        [FromQuery] GetUserByIdQuery query)
    {
        return await ProcessApiCallAsync<GetUserByIdQuery, UserDto>(query);
    }

    [Auth(Roles.Admin)]
    [HttpGet("get-all")]
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<PagedResult<UserDto>> GetAllAsync(
        [FromQuery] GetUsersQuery query)
    {
        return await ProcessApiCallAsync<GetUsersQuery, PagedResult<UserDto>>(query);
    }

    [Auth(Roles.User)]
    [HttpGet("get-public-info-by-id")]
    [ProducesResponseType(typeof(PublicUserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<PublicUserInfoDto> GetPublicUserInfoByIdAsync(
        [FromQuery] GetUserByIdQuery query)
    {
        return await ProcessApiCallAsync<GetUserByIdQuery, PublicUserInfoDto>(query);
    }

    [Auth(Roles.User)]
    [HttpGet("get-id-by-email")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<Guid> GetUserIdByIdAsync(
        [FromQuery] GetUserByLoginQuery query)
    {
        var result = await ProcessApiCallAsync
            <GetUserByLoginQuery, UserDto>(query);

        return result.Id;
    }

    [Auth(Roles.Admin)]
    [HttpGet("get-by-login")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> GetByLoginAsync(
        [FromQuery] GetUserByLoginQuery query)
    {
        return await ProcessApiCallAsync<GetUserByLoginQuery, UserDto>(query);
    }

    [Auth(Roles.Admin)]
    [HttpGet("is-email-taken")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<bool> CheckIsEmailTakenAsync(
        [FromQuery] CheckIsEmailTakenQuery query)
    {
        return await ProcessApiCallAsync<CheckIsEmailTakenQuery, bool>(query);
    }

    [Auth(Roles.Admin)]
    [HttpPost("create")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> CreateWithCredentialsAsync(
        [FromBody] CreateUserCommand createCommand)
    {
        return await ProcessApiCallAsync<CreateUserCommand, UserDto>(createCommand);
    }

    [Auth(Roles.User)]
    [HttpPut("update")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> UpdateUserAsync(
        [FromBody] UpdateUserCommand command)
    {
        return await ProcessApiCallAsync<UpdateUserCommand, UserDto>(command);
    }

}
