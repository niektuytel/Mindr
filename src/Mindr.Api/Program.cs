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

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
        );
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
            endpoints.MapHangfireDashboard();
        });
        app.UseHealthChecks("/healthy");
        app.Run();
    }
}