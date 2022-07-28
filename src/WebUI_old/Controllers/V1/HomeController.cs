using AutoMapper;
using Defender.UserManagement.WebUI_old.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.UserManagement.Application.Modules.Home.Queries;
using Defender.UserManagement.Application.Enums;
using Defender.UserManagement.Domain.Models;

namespace Defender.UserManagement.WebUI_old.Controllers.V1;

public class HomeController : BaseApiController
{
    public HomeController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    [Auth(Roles.User)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<object> HealthCheckAsync()
    {
        return new { Status = "Healthy" };
    }

    [Auth(Roles.Admin, Roles.SuperAdmin)]
    [HttpGet("configuration")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<Dictionary<string, string>> GetConfigurationAsync(ConfigurationLevel configurationLevel)
    {
        var query = new GetConfigurationQuery()
        {
            Level = configurationLevel
        };

        return await ProcessApiCallWithoutMappingAsync<GetConfigurationQuery, Dictionary<string, string>>(query);
    }
}
