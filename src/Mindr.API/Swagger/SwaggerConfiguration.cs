using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mindr.Api.Swagger;

public static class SwaggerConfiguration
{
    public static void AddSwaggerTools(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenNewtonsoftSupport();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Mindr API",
                Description = "Api to manage mindr functionality"
            });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{configuration["IdentityServer:Authority"]}/connect/authorize"),
                        TokenUrl = new Uri($"{configuration["IdentityServer:Authority"]}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            {configuration["IdentityServer:Audience"], "Mindr API - access"}
                        }
                    }
                }

            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { configuration["IdentityServer:Audience"] }
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFullFile = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (!Directory.Exists(AppContext.BaseDirectory))
            {
                Directory.CreateDirectory(xmlFullFile);
            }
            else if(!File.Exists(xmlFullFile))
            {
                File.Create(xmlFullFile);
            }

            options.DocumentFilter<LowercaseDocumentFilter>();
            options.OperationFilter<AuthorizeCheckOperationFilter>();
            options.IncludeXmlComments(xmlFullFile);
        });
    }

    public static void UseSwaggerTools(this WebApplication? app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            // Enable OAuth2.0 authentication in Swagger UI
            c.OAuthClientId(configuration["IdentityServer:ClientId"]);
            c.OAuthAppName("Mindr API - Swagger");
            c.OAuthScopes(configuration["IdentityServer:Audience"]);
            c.OAuthUsePkce();
        });
    }
}
