using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Api.Swagger;
using Hangfire;
using Hangfire.SqlServer;
using Mindr.Shared.Enums;
using Mindr.Api.Services.ConnectorEvents;
using Mindr.Api.Services.Connectors;
using Mindr.HttpRunner.Models;

using Mindr.HttpRunner;
using Microsoft.IdentityModel.Tokens;
using MockedData = Mindr.Api.Persistence.MockedData;
using OpenIddict.Validation.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace Mindr.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // External Services
        builder.Services
            .AddDbContext<IApplicationContext, ApplicationContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"));
            });
        builder.Services
            .AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }
                )
            );
        builder.Services.AddHangfireServer();// Scoped service!
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddSwaggerTools(builder.Configuration);
        builder.Services.AddHealthChecks();

        // Custom Services
        builder.Services.AddHttpRunner();

        builder.Services.AddScoped<IConnectorEventValidator, ConnectorEventValidator>();
        builder.Services.AddScoped<IConnectorEventDriver, ConnectorEventDriver>();
        builder.Services.AddScoped<IConnectorEventManager, ConnectorEventManager>();

        builder.Services.AddScoped<IConnectorValidator, ConnectorValidator>();
        builder.Services.AddScoped<IConnectorManager, ConnectorManager>();
        builder.Services.AddScoped<IConnectorDriver, ConnectorDriver>();

        // Authentication

        // Register the OpenIddict validation components.
        builder.Services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the address of the introspection endpoint.
                options.SetIssuer("https://localhost:44319/");
                options.AddAudiences("resource_server_1");

                // Configure the validation handler to use introspection and register the client
                // credentials used when communicating with the remote introspection endpoint.
                options.UseIntrospection()
                       .SetClientId("resource_server_1")
                       .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");

                // Register the System.Net.Http integration.
                options.UseSystemNetHttp();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        builder.Services.AddAuthorization();

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //  .AddJwtBearer(options =>
        //  {
        //      options.Authority = builder.Configuration["IdentityServer:Authority"];
        //      options.Audience = builder.Configuration["IdentityServer:Audience"];
        //  });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("https://localhost:7163", "https://localhost:44348")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });


        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            #region Set Test Data
            // https://jasonwatmore.com/post/2022/02/01/net-6-execute-ef-database-migrations-from-code-on-startup
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var created = context.Database.EnsureCreated();
            if(created)
            {
                context.Connectors.Add(MockedData.GetConnector1());
                context.Connectors.Add(MockedData.GetConnector2());
                context.ConnectorEvents.Add(MockedData.GetConnectorEvent1());
                context.ConnectorEvents.Add(MockedData.GetConnectorEvent2());
                context.SaveChanges();
            }
            #endregion
        }

        app.UseHangfireDashboard();
        app.UseSwaggerTools(builder.Configuration);

        app.UseCors();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
            endpoints.MapHangfireDashboard();
        });
        app.UseHealthChecks("/healthy");
        app.MapGet("/api/DantooineApi", [Authorize] () => new string[] { "data1", "data2" });
        app.Run();
    }
}