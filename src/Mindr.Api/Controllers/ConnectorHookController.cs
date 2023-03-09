using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("[controller]")]
public class ConnectorHookController : ControllerBase
{
    [HttpGet("all")]
    public IEnumerable<ConnectorHook> GetAllHooks()
    {
        return Enumerable.Range(1, 5).Select(index => new ConnectorHook
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            EventId = $"Identifier {index}",
            ConnectorId = Guid.NewGuid()
        });
    }

}
