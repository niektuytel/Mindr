using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Dantooine.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Dantooine.Server;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        await RegisterApplicationsAsync(scope.ServiceProvider);
        await RegisterScopesAsync(scope.ServiceProvider);

        static async Task RegisterApplicationsAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

            // API
            
            //public static IEnumerable<IdentityResource> IdentityResources =>
            //    new IdentityResource[]
            //    {
            //        new IdentityResources.OpenId(),
            //        new IdentityResources.Profile()
            //    };

            //public static IEnumerable<ApiScope> ApiScopes =>
            //    new[]
            //    {
            //        new ApiScope("mindr_api", "")
            //    };

            //public static IEnumerable<ApiResource> ApiResources =>
            //    new[]
            //    {
            //        new ApiResource("mindr_api", "Mindr Api") {Scopes = {"mindr_api"}}
            //    };

            //public static IEnumerable<Client> Clients =>
            //    new[]
            //    {
            //        new Client
            //        {
            //            ClientId = "Mindr.Api",
            //            ClientName = "Swagger UI for Mindr api",
            //            ClientSecrets = {new Secret("mindr_api".Sha256())}, // change me!
            //            AllowedGrantTypes = GrantTypes.Code,
            //            RequirePkce = true,
            //            RequireClientSecret = false,
            //            RedirectUris = {""},
            //            AllowedCorsOrigins = {"https://localhost:7155"},
            //            AllowedScopes = { "mindr_api" }
            //        }
            //    };
            if (await manager.FindByClientIdAsync("Mindr.Api") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "Mindr.Api",
                    ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:7155/swagger/oauth2-redirect.html")
                    },
                    
                };

                await manager.CreateAsync(descriptor);
            }

            // CLIENT
            if (await manager.FindByClientIdAsync("blazorcodeflowpkceclient") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "blazorcodeflowpkceclient",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Blazor code PKCE",
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/logout/local")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/login/local")
                    },
                    ClientSecret = "codeflow_pkce_client_secret",
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "api1"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }
        }

        static async Task RegisterScopesAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("mindr_api") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "Access to Mindr API",
                    //DisplayNames =
                    //{
                    //    [CultureInfo.GetCultureInfo("En-FR")] = "Accès à l'API de démo"
                    //},
                    Name = "mindr_api",
                    Resources =
                    {
                        //"resource_server_1"
                        //Permissions.Scopes.Id,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Prefixes.Scope + "mindr_api"
                        //new IdentityResources.OpenId(),
                        //new IdentityResources.Profile()
                    }
                });
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
