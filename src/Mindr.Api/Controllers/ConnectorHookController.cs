using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorHookController : BaseController
{
    private readonly IMapper _mapper;

    public ConnectorHookController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Required role: 'ATM Admin'
    /// </summary>
    /// <remarks>
    /// Get the details of a registered Afas AppConnector.  
    /// 
    /// AfasEnv types:
    /// - Production = 0 (default)
    /// - Test       = 1
    /// - Accept     = 2
    /// 
    /// </remarks>
    /// <item code="200">Found connector</item>
    /// <item code="400">Invalid item</item>
    /// <item code="401">Unauthorized</item>
    /// <item code="403">Forbidden, Missing role 'Atm.Admin'</item>
    /// <item code="404">AppConnector not found</item>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ConnectorHook>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult GetAll()
    {
        var items = ItemHooks;
        return Ok(items);
    }

    /// <summary>
    /// Required role: 'ATM Admin'
    /// </summary>
    /// <remarks>
    /// Get the details of a registered Afas AppConnector.  
    /// 
    /// AfasEnv types:
    /// - Production = 0 (default)
    /// - Test       = 1
    /// - Accept     = 2
    /// 
    /// </remarks>
    /// <item code="200">Found connector</item>
    /// <item code="400">Invalid item</item>
    /// <item code="401">Unauthorized</item>
    /// <item code="403">Forbidden, Missing role 'Atm.Admin'</item>
    /// <item code="404">AppConnector not found</item>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<ConnectorHook>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult Upsert([FromBody]ConnectorHook payload)
    {
        ItemHooks.Add(payload);
        return Ok();
    }

    /// <summary>
    /// Required role: 'ATM Admin'
    /// </summary>
    /// <remarks>
    /// Get the details of a registered Afas AppConnector.  
    /// 
    /// AfasEnv types:
    /// - Production = 0 (default)
    /// - Test       = 1
    /// - Accept     = 2
    /// 
    /// </remarks>
    /// <item code="200">Found connector</item>
    /// <item code="400">Invalid item</item>
    /// <item code="401">Unauthorized</item>
    /// <item code="403">Forbidden, Missing role 'Atm.Admin'</item>
    /// <item code="404">AppConnector not found</item>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IEnumerable<ConnectorHook>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult Delete(Guid id)
    {
        return Ok();
    }

}
