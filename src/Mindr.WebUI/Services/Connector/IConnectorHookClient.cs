using Mindr.Core.Models.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services.Connector;

public interface IConnectorHookClient
{
    Task<HttpResponseMessage?> Delete(Guid hookid);
    Task<HttpResponseMessage?> Upsert(ConnectorHook hook);
}