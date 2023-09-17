using Defender.UserManagementService.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.UserManagementService.Application.Configuration.Exstension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

        return services;
    }
}