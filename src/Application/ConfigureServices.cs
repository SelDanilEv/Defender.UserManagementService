using System.Reflection;
using Defender.UserManagementService.Application.Common.Interfaces.Services;
using Defender.UserManagementService.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.UserManagementService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IUserManagementService, Services.UserManagementService>();
        services.AddTransient<IAccessCodeService, AccessCodeService>();

        return services;
    }
}
