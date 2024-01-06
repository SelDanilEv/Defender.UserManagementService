using Defender.Common.Clients.Identity;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Repositories;
using Defender.UserManagementService.Application.Configuration.Options;
using Defender.UserManagementService.Infrastructure.Clients.Identity;
using Defender.UserManagementService.Infrastructure.Clients.Interfaces;
using Defender.UserManagementService.Infrastructure.Repositories.UserInfos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.UserManagementService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterClientWrappers()
            .RegisterServices()
            .RegisterRepositories()
            .RegisterApiClients();

        return services;
    }

    private static IServiceCollection RegisterClientWrappers(this IServiceCollection services)
    {
        services.AddTransient<IIdentityWrapper, IdentityWrapper>();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IUserManagementService, Services.UserManagementService>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IUserInfoRepository, UserInfoRepository>();

        return services;
    }

    private static IServiceCollection RegisterApiClients(this IServiceCollection services)
    {
        services.RegisterIdentityClient(
            (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value.Url);
            });

        return services;
    }

}
