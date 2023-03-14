using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mindr.Api.Swagger;

internal static class SwaggerConfiguration
{
    internal static void AddSwaggerTools(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenNewtonsoftSupport();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(options =>
        {
            var baseUrl = configuration["AzureAd:Instance"];
            var tenantId = configuration["AzureAd:TenantId"];
            var authUrl = $"{baseUrl}/{tenantId}/oauth2/v2.0/authorize";
            var tokenUrl = $"{baseUrl}/{tenantId}/oauth2/v2.0/token";

            var openApiScheme = new OpenApiSecurityScheme
            {
                Name = "oauth",
                Description = "Azure login, OAuth2.0 auth code with PKCE",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },

                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(authUrl),
                        TokenUrl = new Uri(tokenUrl)
                    }
                }
            };
            var openApiRequirements = new OpenApiSecurityRequirement
            {
                { openApiScheme, new List<string>{ }}
            };

            options.DocumentFilter<LowercaseDocumentFilter>();
            options.AddSecurityDefinition("oauth2", openApiScheme);
            options.AddSecurityRequirement(openApiRequirements);
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Mindr API",
                Description = "Api to manage mindr functionality"
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }

    internal static void UseSwaggerTools(this WebApplication? app, IConfiguration configuration)
    {
        //// WARN: Configure the HTTP request pipeline. >> if (app.Environment.IsDevelopment())

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            //c.OAuthClientSecret(configuration["AzureAd:SlientSecret"]);

            var clientId = configuration["AzureAd:ClientId"];
            var scope = $"api://{clientId}/access_as_user";

            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ATM Api(v1)");
            c.OAuthClientId(clientId);
            c.OAuthClientSecret(configuration["AzureAd:ClientSecret"]);
            c.OAuthScopes(scope);
        });
    }
}
