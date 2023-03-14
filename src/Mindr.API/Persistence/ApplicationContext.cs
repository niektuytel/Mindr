using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.Connector;
using NuGet.Common;

namespace Mindr.Api.Persistence;

public class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<ConnectorHook> ConnectorHooks => Set<ConnectorHook>();

}
