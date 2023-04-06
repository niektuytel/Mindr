using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
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

    public virtual DbSet<Connector> Connectors => Set<Connector>();
    public virtual DbSet<ConnectorVariable> ConnectorVariables => Set<ConnectorVariable>();
    public virtual DbSet<HttpItem> HttpItems => Set<HttpItem>();
    public virtual DbSet<ConnectorEvent> ConnectorEvents => Set<ConnectorEvent>();


}
