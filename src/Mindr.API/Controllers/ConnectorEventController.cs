using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorEventController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorEventController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    // TODO: Play responses to user from exception

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
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult GetAll()
    {
        var items = _context.ConnectorEvents.ToArray();
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
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult Upsert([FromBody]ConnectorEvent payload)
    {
        _context.ConnectorEvents.Add(payload);
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
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public IActionResult Delete(Guid id)
    {
        var entity = _context.ConnectorEvents.FirstOrDefault(x => x.Id == id);
        if (entity == null) 
        {
            return NotFound();
        }

        _context.ConnectorEvents.Remove(entity);
        _context.SaveChanges();
        return Ok();
    }

}
