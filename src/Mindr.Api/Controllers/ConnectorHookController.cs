using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorHookController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorHookController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }

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
        var items = _context.ConnectorHooks.ToArray();
        return Ok(items);
    }

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
        _context.ConnectorHooks.Add(payload);
        _context.SaveChanges();

        return Ok();
    }

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
        var entity = _context.ConnectorHooks.FirstOrDefault(x => x.Id == id);
        if (entity == null) 
        {
            return NotFound();
        }

        _context.ConnectorHooks.Remove(entity);
        _context.SaveChanges();
        return Ok();
    }

}
