using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.UserManagement.Application.Models.LoginResponse;
using Defender.UserManagement.Application.Modules.Auth.Commands;

namespace Defender.UserManagement.WebUI.Controllers.V1;

public class AuthController : BaseApiController
{
    public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("google")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<LoginResponse> GenerateJWTFromGoogleAsync([FromBody] LoginGoogleCommand loginGoogleCommand)
    {
        return await ProcessApiCallWithoutMappingAsync<LoginGoogleCommand, LoginResponse>(loginGoogleCommand);
    }
}
