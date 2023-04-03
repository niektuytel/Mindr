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
using Mindr.Core.Enums;
using Mindr.Api.Services;

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
            .AddDbContext<IApplicationContext, ApplicationContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
        builder.Services.AddScoped<IHttpCollectionFactory, HttpCollectionFactory>();
        builder.Services.AddScoped<IHttpCollectionClient, HttpCollectionClient>();
        builder.Services.AddScoped<IConnectorEventClient, ConnectorEventClient>();
        builder.Services.AddScoped<IConnectorClient, ConnectorClient>();


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
                var httpItem1 = new HttpItem()
                {
                    Name = "Send Sample Text Message",
                    Description = "Sample text",
                    Request = new()
                    {
                        Variables = new List<HttpVariable>()
                        {
                            new()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new()
                            {
                                Location = VariablePosition.Header,
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                            },
                            new()
                            {
                                Location = VariablePosition.Body,
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },

                        Method = "POST",
                        Url = new()
                        {
                            Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                            Protocol = "https",
                            Hosts = new string[]
                                {
                                        "graph",
                                        "facebook",
                                        "com"
                                },
                            Paths = new string[]
                                {
                                    "{{Version}}",
                                    "{{Phone-Number-ID}}",
                                    "messages"
                                }
                        },
                        Header = new HttpHeader[]
                        {
                            new()
                            {
                                Key = "Authorization",
                                Value = "Bearer {{User-Access-Token}}",
                                Type = "text"
                            },
                            new()
                            {
                                Key = "Content-Type",
                                Value = "application/json",
                                Type = "text"
                            }
                        },
                        Body = new()
                        {
                            Mode = "raw",
                            Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                            Options = new()
                            {
                                Raw = new()
                                {
                                    Language = "json"
                                }
                            }
                        }
                    }
                };
                var httpItem2 = new HttpItem()
                {
                    Name = "Send Sample Text Message",
                    Description = "Sample text 2",
                    Request = new()
                    {
                        Variables = new List<HttpVariable>()
                        {
                            new()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new()
                            {
                                Location = VariablePosition.Header,
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                            },
                            new()
                            {
                                Location = VariablePosition.Body,
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },

                        Method = "POST",
                        Url = new()
                        {
                            Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                            Protocol = "https",
                            Hosts = new string[]
                                {
                                        "graph",
                                        "facebook",
                                        "com"
                                },
                            Paths = new string[]
                                {
                                    "{{Version}}",
                                    "{{Phone-Number-ID}}",
                                    "messages"
                                }
                        },
                        Header = new HttpHeader[]
                        {
                            new()
                            {
                                Key = "Authorization",
                                Value = "Bearer {{User-Access-Token}}",
                                Type = "text"
                            },
                            new()
                            {
                                Key = "Content-Type",
                                Value = "application/json",
                                Type = "text"
                            }
                        },
                        Body = new()
                        {
                            Mode = "raw",
                            Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                            Options = new()
                            {
                                Raw = new()
                                {
                                    Language = "json"
                                }
                            }
                        }
                    }
                };

                var Connector1 = new Connector()
                {
                    Id = Guid.Parse("c98d9b51-cf20-4938-b7cb-76e8743f673c"),
                    CreatedBy = "00000000-0000-0000-aacc-c311156d0357",
                    Color = "orange",
                    Name = "Send Whatsapp Text Message",
                    Description = "Some description explain the product",
                    Variables = new ConnectorVariable[]
                        {
                            new()
                            {
                                InputByUser = false,
                                Name = "Authentication Token",
                                Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                            },
                            new()
                            {
                                InputByUser = false,
                                Name = "Api version",
                                Description = "Api Version",
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new()
                            {
                                InputByUser = true,
                                Name = "Sender Phone id",
                                Description = "The phone number id of the sender",
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new()
                            {
                                InputByUser = true,
                                Name = "Receiver Phone number id",
                                Description = "The phone number id of the receiver",
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },
                    Pipeline = new List<HttpItem>() { httpItem1, httpItem2 }
                };
                context.Connectors.Add(Connector1);

                var Connector2 = new Connector()
                {
                    Id = Guid.Parse("60994748-0cf3-452b-bbbc-44930e8fb052"),
                    CreatedBy = "00000000-0000-0000-aacc-c311156d0357",
                    Color = "blue",
                    Name = "Send WhatsApp Sample Text Message",
                    Description = "Some description explain the product",
                    Variables = new ConnectorVariable[]
                        {
                        new()
                        {
                            InputByUser = false,
                            Name = "Authentication Token",
                            Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                            Key = "User-Access-Token",
                            Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                        },
                        new()
                        {
                            InputByUser = false,
                            Name = "Api version",
                            Description = "Api Version",
                            Key = "Version",
                            Value = "v15.0"
                        },
                        new()
                        {
                            InputByUser = false,
                            Name = "Sender Phone id",
                            Description = "The phone number id of the sender",
                            Key = "Phone-Number-ID",
                            Value = "113037821608895"
                        },
                        new()
                        {
                            InputByUser = true,
                            Name = "Receiver Phone number id",
                            Description = "The phone number id of the receiver",
                            Key = "Recipient-Phone-Number",
                            Value = "31618395668"
                        },
                        new()
                        {
                            InputByUser = true,
                            Name = "Sending message",
                            Description = "Message that will been sended",
                            Key = "Text-Body-String",
                            Value = "unser inputed content"
                        }
                    },
                    Pipeline = new List<HttpItem>() { httpItem1, httpItem2 }
                };
                context.Connectors.Add(Connector2);

                // seed database
                var event1 = new ConnectorEvent("00000000-0000-0000-aacc-c311156d0357", "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector1);
                context.ConnectorEvents.Add(event1); //Test 1

                var event2 = new ConnectorEvent("00000000-0000-0000-aacc-c311156d0357", "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector2);
                context.ConnectorEvents.Add(event2); //Test 2
                
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