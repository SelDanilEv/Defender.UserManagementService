using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Application.Common.Interfaces.Repositories.Users;
using Defender.UserManagement.Application.Configuration.Options;
using Defender.UserManagement.Infrastructure.Clients;
using Defender.UserManagement.Infrastructure.Clients.Interfaces;
using Defender.UserManagement.Infrastructure.Repositories.Users;
using Defender.UserManagement.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.UserManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserManagementService, UserManagementService>();
        services.AddTransient<IGoogleTokenValidationService, GoogleTokenValidationService>();

        services.AddSingleton<IUserRepository, UserRepository>();

        RegisterApiClients(services);

        return services;
    }

    private static void RegisterApiClients(IServiceCollection services)
    {
        services.AddHttpClient<IGoogleClient, GoogleClient>("GoogleClient", (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<GoogleOption>>().Value.Url);
        });
    }

}
