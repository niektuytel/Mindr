using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Mindr.Server.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authentication.Google;
using OpenIddict.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Mindr.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
        => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

        services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration[$"{nameof(GoogleOptions)}:ClientId"];
                googleOptions.ClientSecret = Configuration[$"{nameof(GoogleOptions)}:ClientSecret"];
                googleOptions.CallbackPath = Configuration[$"{nameof(GoogleOptions)}:CallbackPath"];
            });

        services.AddDatabaseDeveloperPageExceptionFilter();

        // Register the Identity services.
        services.AddIdentity<Domain.OpenId.ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();


        // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
        // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddOpenIddict()
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>();

                options.UseQuartz();
            })
            .AddClient(options =>
            {
                // Allow the OpenIddict client to negotiate the authorization code flow.
                options.AllowAuthorizationCodeFlow();

                // Register the signing and encryption credentials used to protect
                // sensitive data like the state tokens produced by OpenIddict.
                options.AddEphemeralEncryptionKey()
                       .AddEphemeralSigningKey();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableRedirectionEndpointPassthrough();

                // Register the GitHub integration.
                options.UseWebProviders()
                        .UseGoogle(options =>
                        {
                            options.SetClientId(Configuration[$"{nameof(GoogleOptions)}:ClientId"])
                                .SetClientSecret(Configuration[$"{nameof(GoogleOptions)}:ClientSecret"])
                                .SetRedirectUri(Configuration[$"{nameof(GoogleOptions)}:CallbackPath"]);
                        });
            })
            .AddServer(options =>
            {
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetLogoutEndpointUris("connect/logout")
                       .SetIntrospectionEndpointUris("connect/introspect")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserinfoEndpointUris("connect/userinfo")
                       .SetVerificationEndpointUris("connect/verify");

                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                   Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

                // Note: this sample only uses the authorization code flow but you can enable
                // the other flows if you need to support implicit, password or client credentials.
                options.AllowAuthorizationCodeFlow();

                // Register the signing and encryption credentials.
                options.AddEphemeralEncryptionKey()
                       .AddEphemeralSigningKey();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .EnableStatusCodePagesIntegration();
            })
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        // Register the worker responsible for seeding the database.
        // Note: in a real world application, this step should be part of a setup script.
        services.AddHostedService<Worker>();

        // HACK: This can cause security risks!
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyHeader();
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();

            // https://jasonwatmore.com/post/2022/02/01/net-6-execute-ef-database-migrations-from-code-on-startup
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var created = context.Database.EnsureCreated();
        }
        else
        {
            app.UseStatusCodePagesWithReExecute("~/error");
            //app.UseExceptionHandler("~/error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCors();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }

}
