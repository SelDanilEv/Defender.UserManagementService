﻿using System;
using System.Net.Http;
using System.Text;
using Defender.Common.Accessors;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Helpers;
using Defender.Common.Interfaces;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace Defender.UserManagementService.WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddSingleton<IAccountAccessor, AccountAccessor>();

        services.AddHttpContextAccessor();

        services.AddProblemDetails(options => ConfigureProblemDetails(options, environment));

        services.AddJwtAuthentication(configuration);

        services.AddSwagger();

        services.AddFluentValidationAutoValidation();

        services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            x.JsonSerializerOptions.IgnoreNullValues = true;
        });

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = configuration["JwtTokenIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        SecretsHelper.GetSecret(Common.Enums.Secret.JwtSecret)))
            };
        });

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
                Description = "Service to manage user personal info",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample: \"1safsfsdfdfd\"",
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
        options.IncludeExceptionDetails = (ctx, ex) => environment.IsEnvironment("Development") || environment.IsEnvironment("DockerDev");

        options.Map<ValidationException>(exception =>
        {
            var validationProblemDetails = new ValidationProblemDetails(exception.Errors)
            {
                Detail = exception.Message,
                Status = StatusCodes.Status422UnprocessableEntity
            };

            return validationProblemDetails;
        });

        options.Map<ForbiddenAccessException>(exception =>
        {
            var problemDetails = new ProblemDetails
            {
                Detail = exception.Message,
                Status = StatusCodes.Status403Forbidden
            };
            return problemDetails;
        });

        options.Map<ServiceException>(exception =>
        {
            var problemDetails = new ProblemDetails
            {
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest
            };
            return problemDetails;
        });

        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

        options.Map<Exception>(exception =>
        {
            var problemDetails = new ProblemDetails
            {
                Detail = ErrorCodeHelper.GetErrorCode(ErrorCode.UnhandledError),
                Status = StatusCodes.Status500InternalServerError
            };
            return problemDetails;
        }); ;
    }

}