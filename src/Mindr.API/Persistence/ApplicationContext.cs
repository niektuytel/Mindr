using Microsoft.EntityFrameworkCore;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.PersonalCredential;
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

    public virtual DbSet<PersonalCredential> PersonalCredentials => Set<PersonalCredential>();

    public virtual DbSet<Connector> Connectors => Set<Connector>();
    public virtual DbSet<ConnectorVariable> ConnectorVariables => Set<ConnectorVariable>();
    public virtual DbSet<ConnectorEvent> ConnectorEvents => Set<ConnectorEvent>();
    public virtual DbSet<HttpItem> HttpItems => Set<HttpItem>();

}
