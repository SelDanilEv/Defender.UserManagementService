using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Infrastructure.Repositories.UserInfos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.UserManagementService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterServices(services);

        RegisterRepositories(services);

        RegisterApiClients(services);

        return services;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IUserManagementService, Services.UserManagementService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
    }

    private static void RegisterApiClients(IServiceCollection services)
    {

    }

}
