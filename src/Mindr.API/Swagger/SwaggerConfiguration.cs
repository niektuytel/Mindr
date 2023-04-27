using Microsoft.EntityFrameworkCore;
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

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(options =>
        {

            // Configure Swagger to use the OpenAPI 2.0 security scheme
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:7163/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:7163/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "Mindr.ServerAPI", "Mindr.ServerAPI" }
                        }
                    }
                }
            });

            // Require authentication for all API methods
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
                    new[] { "Mindr.ServerAPI" }
                }
            });
            //var baseUrl = configuration["AzureAd:Instance"];
            //var tenantId = configuration["AzureAd:TenantId"];
            //var authUrl = $"{baseUrl}/{tenantId}/oauth2/v2.0/authorize";
            //var tokenUrl = $"{baseUrl}/{tenantId}/oauth2/v2.0/token";

            //var openApiScheme = new OpenApiSecurityScheme
            //{
            //    Name = "oauth",
            //    Description = "Azure login, OAuth2.0 auth code with PKCE",
            //    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },

            //    Type = SecuritySchemeType.OAuth2,
            //    Flows = new OpenApiOAuthFlows
            //    {
            //        Implicit = new OpenApiOAuthFlow
            //        {
            //            AuthorizationUrl = new Uri(authUrl),
            //            TokenUrl = new Uri(tokenUrl)
            //        }
            //    }
            //};
            //var openApiRequirements = new OpenApiSecurityRequirement
            //{
            //    { openApiScheme, new List<string>{ }}
            //};

            options.DocumentFilter<LowercaseDocumentFilter>();
            //options.AddSecurityDefinition("oauth2", openApiScheme);
            //options.AddSecurityRequirement(openApiRequirements);
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

    public static void UseSwaggerTools(this WebApplication? app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            // Enable OAuth2.0 authentication in Swagger UI
            c.OAuthClientId("Mindr.Api");
            c.OAuthAppName("Mindr Identity Server");
            c.OAuthUsePkce();
        });
    }
}
