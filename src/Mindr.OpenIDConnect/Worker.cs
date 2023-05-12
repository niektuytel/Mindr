using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Mindr.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Mindr.Server;

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
            if (await manager.FindByClientIdAsync("resource_server_1") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "resource_server_1",
                    ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            // Blazor Hosted
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

            // Api Swagger
            if (await manager.FindByClientIdAsync("web-client") is null)
            {
                
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "web-client",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Swagger client application",
                    RedirectUris =
                    {
                        new Uri("https://localhost:7155/swagger/oauth2-redirect.html")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:7155/resources")
                    },
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
                       $"{Permissions.Prefixes.Scope}api1"
                    },
                    //Requirements =
                    //{
                    //    Requirements.Features.ProofKeyForCodeExchange
                    //}
                });
            }
        }

        static async Task RegisterScopesAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("api1") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "Dantooine API access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                    },
                    Name = "api1",
                    Resources =
                    {
                        "resource_server_1"
                    }
                });
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
