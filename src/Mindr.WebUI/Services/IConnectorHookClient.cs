using Mindr.Core.Models.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services;

public interface IConnectorHookClient
{
    Task<HttpResponseMessage> Delete(Guid hookid, string aztoken);
    Task<HttpResponseMessage> Upsert(ConnectorHook hook, string aztoken);
}