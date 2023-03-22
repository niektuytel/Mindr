using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? eventId = null, [FromQuery] string? query = null)
    {
        var items = Items;
        if(!string.IsNullOrEmpty(eventId))
        {
            var connectorIds = _context.ConnectorHooks.Where(item => item.EventId == eventId && item.UserId == Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec")).Select(item => item.ConnectorId);

            items = Items.Where(item => connectorIds.Contains(item.Id));
        }
        else if (!string.IsNullOrEmpty(query))
        {
            items = Items.Where(item => item.Name.ToLower().Contains(query));
        }

        var response = _mapper.Map<IEnumerable<ConnectorBriefDTO>>(items);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var connector = Items.FirstOrDefault(item => item.Id == id);
        if(connector == null)
        {
            return NotFound();
        }

        return Ok(connector);
    }

    [HttpPost]
    public IActionResult Insert(Connector payload)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Connector payload)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        return Ok();
    }

}
