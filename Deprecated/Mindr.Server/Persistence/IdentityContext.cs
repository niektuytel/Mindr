using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mindr.Server.Models;

namespace Mindr.Server.Data
{
    public class IdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}