using Defender.UserManagement.Application.Enums;

namespace Defender.UserManagement.Application.Helpers;

public static class EnvVariableResolver
{
    private static readonly Dictionary<string, string> _environmentVariables = new Dictionary<string, string>();

    public static string GetEnvironmentVariable(EnvVariable envVariable)
    {
        return GetEnvironmentVariable(MapEnvVariableToKey(envVariable));
    }

    public static string GetEnvironmentVariable(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            SimpleLogger.Log($"Trying to get env variable by empty key", SimpleLogger.LogLevel.Debug);
            return string.Empty;
        }

        if (_environmentVariables.ContainsKey(key))
        {
            return _environmentVariables[key];
        }

        var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process) ??
            Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User) ??
            Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);

        if (value == null)
        {
            SimpleLogger.Log($"Environment variable is not found: {key}", SimpleLogger.LogLevel.Warning);
            return string.Empty;
        }

        _environmentVariables.Add(key, value);

        return value;
    }

    private static string MapEnvVariableToKey(EnvVariable envVariable) => "Defender_App_" + envVariable.ToString();
}
