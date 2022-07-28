using Defender.UserManagement.Application.Common.Exceptions;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.WebUI.Filters;
using Defender.UserManagement.WebUI.Services;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddProblemDetails(options => ConfigureProblemDetails(options, environment));

        services.AddSwagger();

        services.AddControllers(options =>
             options.Filters.Add<ApiExceptionFilterAttribute>())
                 .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "User management service",
                Description = "Service to manage users and generate jwt token",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }

    private static void ConfigureProblemDetails(ProblemDetailsOptions options, IWebHostEnvironment environment)
    {
        options.IncludeExceptionDetails = (ctx, ex) => (environment.IsEnvironment("Development"));

        options.Map<ValidationException>(exception =>
        {
            var validationProblemDetails = new ValidationProblemDetails(exception.Errors);
            validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            return validationProblemDetails;
        });

        options.Map<ForbiddenAccessException>(exception =>
        {
            var problemDetails = new ProblemDetails();

            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status403Forbidden;
            return problemDetails;
        });

        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    }

}
