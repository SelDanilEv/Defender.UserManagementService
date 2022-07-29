using Defender.UserManagement.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.UserManagement.Application.Configuration.Exstension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOption>(configuration.GetSection(nameof(MongoDbOption)));

        services.Configure<GoogleOption>(configuration.GetSection(nameof(GoogleOption)));

        return services;
    }
}