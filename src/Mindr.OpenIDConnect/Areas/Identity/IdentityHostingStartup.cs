using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mindr.Domain.OpenId;
using Mindr.Server.Data;

[assembly: HostingStartup(typeof(Mindr.OpenIDConnect.Areas.Identity.IdentityHostingStartup))]
namespace Mindr.OpenIDConnect.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
