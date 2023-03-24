using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Api.Swagger;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Graph.ExternalConnectors;
using Mindr.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Mindr.Core.Services.Connectors;

namespace Mindr.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // External Services
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
        builder.Services
            .AddDbContext<IApplicationContext, ApplicationContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"))
            );
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
        builder.Services.AddScoped<IHttpCollectionFactory, HttpCollectionFactory>();
        builder.Services.AddScoped<IHttpCollectionClient, HttpCollectionClient>();
        builder.Services.AddScoped<IConnectorEventClient, ConnectorEventClient>();


        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            // https://jasonwatmore.com/post/2022/02/01/net-6-execute-ef-database-migrations-from-code-on-startup
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var created = context.Database.EnsureCreated();
            if(created)
            {
                var Connector1 = new Connector()
                {
                    Id = Guid.Parse("c98d9b51-cf20-4938-b7cb-76e8743f673c"),
                    Color = "orange",
                    Name = "Send Whatsapp Text Message",
                    Description = "Some description explain the product",
                    Variables = new ConnectorParam[]
                        {
                            new()
                            {
                                Name = "Authentication Token",
                                Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                            },
                            new()
                            {
                                Name = "Api version",
                                Description = "Api Version",
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new()
                            {
                                Name = "Sender Phone id",
                                Description = "The phone number id of the sender",
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new()
                            {
                                Name = "Receiver Phone number id",
                                Description = "The phone number id of the receiver",
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },
                    //Pipeline = new List<HttpItem>() { HttpItem1, HttpItem2 }
                };
                var Connector2 = new Connector()
                {
                    Id = Guid.Parse("60994748-0cf3-452b-bbbc-44930e8fb052"),
                    Color = "blue",
                    Name = "Send WhatsApp Sample Text Message",
                    Description = "Some description explain the product",
                    Variables = new ConnectorParam[]
                        {
                        new()
                        {
                            Name = "Authentication Token",
                            Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                            Key = "User-Access-Token",
                            Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                        },
                        new()
                        {
                            Name = "Api version",
                            Description = "Api Version",
                            Key = "Version",
                            Value = "v15.0"
                        },
                        new()
                        {
                            Name = "Sender Phone id",
                            Description = "The phone number id of the sender",
                            Key = "Phone-Number-ID",
                            Value = "113037821608895"
                        },
                        new()
                        {
                            Name = "Receiver Phone number id",
                            Description = "The phone number id of the receiver",
                            Key = "Recipient-Phone-Number",
                            Value = "31618395668"
                        },
                        new()
                        {
                            Name = "Sending message",
                            Description = "Message that will been sended",
                            Key = "Text-Body-String",
                            Value = "unser inputed content"
                        }
                        },
                    //Pipeline = new List<HttpItem>() { HttpItem1, HttpItem2 }
                };

                // seed database
                context.ConnectorEvents.Add(new ConnectorEvent(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector1)); //Test 1
                context.ConnectorEvents.Add(new ConnectorEvent(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector2)); //Test 2
                //context.ConnectorEvents.Add(new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector2)); //Test 2

                context.SaveChanges();
            }
        }

        app.UseHangfireDashboard();


        //protected List<ConnectorHook> ItemHooks { get; set; } = new List<ConnectorHook>()
        //{
        //    new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector1),//Test 1
        //    new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMQAAAA==", Connector2),//Test 2
        //    new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMQAAAA==", Connector1)//Test 2
        //};



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