using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

public class ConnectorHookController : BaseController
{
    private readonly IMapper _mapper;

    public ConnectorHookController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var items = ItemHooks;
        return Ok(items);
    }

    [HttpGet("{eventId}")]
    public IActionResult GetByEventId(string eventId)
    {
        var items = ItemHooks.Where(item => item.EventId == eventId);
        if (items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    [HttpPost]
    public IActionResult Insert([FromBody]ConnectorHook payload)
    {
        ItemHooks.Add(payload);
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
