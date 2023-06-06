using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Mindr.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Mindr.Server;

public class Worker : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    
    public Worker(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        await RegisterApplicationsAsync(_configuration, scope.ServiceProvider, cancellationToken);
        await RegisterScopesAsync(_configuration, scope.ServiceProvider, cancellationToken);

    }

    private static async Task RegisterApplicationsAsync(IConfiguration configuration, IServiceProvider provider, CancellationToken cancellationToken)
    {
        // Retrieve the array of application descriptors from configuration.
        var registeredApplications = configuration.GetSection("Worker:RegisteredApplications")
            .Get<IEnumerable<OpenIddictApplicationDescriptor>>();

        var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();
        foreach (var app in registeredApplications)
        {
            if (await manager.FindByClientIdAsync(app.ClientId, cancellationToken) != null)
            {
                continue;
            }

            await manager.CreateAsync(app, cancellationToken);
        }

        // Blazor Hosted
        if (await manager.FindByClientIdAsync("blazorcodeflowpkceclient") == null)
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
                    Permissions.Prefixes.Scope + "mindr_api_access"
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            });
        }

    }

    private static async Task RegisterScopesAsync(IConfiguration configuration, IServiceProvider provider, CancellationToken cancellationToken)
    {
        // Retrieve the array of scope descriptors from configuration.
        var registeredScopes = configuration.GetSection("Worker:RegisteredScopes")
            .Get<IEnumerable<OpenIddictScopeDescriptor>>();

        var manager = provider.GetRequiredService<IOpenIddictScopeManager>();
        foreach (var scope in registeredScopes)
        {
            if (await manager.FindByNameAsync(scope.Name, cancellationToken) != null)
            {
                continue;
            }

            await manager.CreateAsync(scope, cancellationToken);
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
