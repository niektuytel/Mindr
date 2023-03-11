using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Persistence
{
    public interface IApplicationContext
    {
        DbSet<ConnectorHook> ConnectorHooks { get; }
    }
}