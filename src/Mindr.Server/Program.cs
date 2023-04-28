using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Mindr.Server;
using Mindr.Server.Data;
using Mindr.Server.Models;
using System.Reflection;

namespace Mindr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Authentication
            var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(defaultConnection));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();

            builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, IdentityContext>();

            builder.Services.AddAuthentication()
                .AddGoogle("Google", options =>
            {
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.ClientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
                options.ClientSecret = "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";
            })
                .AddIdentityServerJwt();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                  builder =>
                  {
                        builder.WithOrigins("https://localhost:7163", "https://localhost:7155")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                  });
            });
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigins");
            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            app.MapFallbackToFile("index.html");
            app.Run();
        }
    }
}