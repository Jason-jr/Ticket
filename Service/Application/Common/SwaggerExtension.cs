using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Common;

public static class SwaggerExtension
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            var apiFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
            var apiPath = Path.Combine(AppContext.BaseDirectory, apiFile);
            c.IncludeXmlComments(apiPath, true);

            var modelFile = $"{nameof(Application)}.xml";
            var modelPath = Path.Combine(AppContext.BaseDirectory, modelFile);
            c.IncludeXmlComments(modelPath);

            c.OperationFilter<CustomOperationFilter>();

            c.CustomSchemaIds(s => s.FullName);
        });
    }

    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            Auth(operation, context);
        }

        static void Auth(OpenApiOperation operation, OperationFilterContext context)
        {
            var isAuthorized = context.MethodInfo.DeclaringType.GetCustomAttributes<AuthorizeAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes<AuthorizeAttribute>().Any();

            var isAllowAnonymous = context.MethodInfo.DeclaringType.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (!isAuthorized || isAllowAnonymous) return;

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JWT" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ jwtbearerScheme ] = Array.Empty<string>()
                    }
                };
        }
    }

}