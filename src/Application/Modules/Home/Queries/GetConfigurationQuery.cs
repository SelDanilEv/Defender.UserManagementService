using System.Collections;
using Defender.UserManagement.Application.Enums;
using Defender.UserManagement.Application.Helpers;
using MediatR;

namespace Defender.UserManagement.Application.Modules.Home.Queries;

public record GetConfigurationQuery : IRequest<Dictionary<string, string>>
{
    public ConfigurationLevel Level { get; set; } = ConfigurationLevel.All;
};

public class GetConfigurationQueryHandler : RequestHandler<GetConfigurationQuery, Dictionary<string, string>>
{
    protected override Dictionary<string, string> Handle(GetConfigurationQuery request)
    {
        var result = new Dictionary<string, string>();

        switch (request.Level)
        {
            case ConfigurationLevel.All:
                var allProcessEnvVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
                foreach (DictionaryEntry envVariable in allProcessEnvVariables)
                {
                    result.TryAdd(envVariable.Key.ToString(), envVariable.Value.ToString());
                }

                var allUserEnvVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
                foreach (DictionaryEntry envVariable in allUserEnvVariables)
                {
                    result.TryAdd(envVariable.Key.ToString(), envVariable.Value.ToString());
                }

                var allMachineEnvVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
                foreach (DictionaryEntry envVariable in allMachineEnvVariables)
                {
                    result.TryAdd(envVariable.Key.ToString(), envVariable.Value.ToString());
                }
                break;
            case ConfigurationLevel.Admin:
                foreach (EnvVariable envVariable in (EnvVariable[])Enum.GetValues(typeof(EnvVariable)))
                {
                    result.Add(envVariable.ToString(), EnvVariableResolver.GetEnvironmentVariable(envVariable));
                }
                break;
        }

        return result;
    }
}
