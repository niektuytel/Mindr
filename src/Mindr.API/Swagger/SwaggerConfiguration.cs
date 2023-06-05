using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Mindr.Api.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Mindr.Api.Swagger;

public static class SwaggerConfiguration
{
    public static void AddSwaggerTools(this IServiceCollection services, IConfiguration configuration)
    {
        var oidcAuthority = configuration[$"{nameof(IdentityServer)}:Authority"];

        services.AddEndpointsApiExplorer();
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
                        AuthorizationUrl = new Uri($"{oidcAuthority}/connect/authorize"),
                        TokenUrl = new Uri($"{oidcAuthority}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "api1", "resource server scope" }
                        }
                    },
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    Array.Empty<string>()
                }
            });


            options.DocumentFilter<LowercaseDocumentFilter>();
            //options.SchemaFilter<FieldsSchemaFilter>();
            options.OperationFilter<AuthorizeCheckOperationFilter>();

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

            options.IncludeXmlComments(xmlFullFile);
        });
    }

    public static void UseSwaggerTools(this WebApplication? app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.DocExpansion(DocExpansion.None);//This will not expand all the API's.

            // Enable OAuth2.0 authentication in Swagger UI
            c.OAuthClientId("web-client");
            c.OAuthClientSecret("901564A5-E7FE-42CB-B10D-61EF6A8F3654");
        });
    }
}
