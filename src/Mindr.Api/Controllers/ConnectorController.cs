using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Extensions;
using Mindr.Api.Persistence;
using Mindr.Api.Services;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorController : BaseController
{
    private readonly IConnectorClient _connectorClient;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorController(IConnectorClient connectorClient, IMapper mapper, ApplicationContext context)
    {
        _connectorClient = connectorClient;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? eventId = null, [FromQuery] string? query = null)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();

                return await _connectorClient.GetAll(userId, eventId, query);
            }
        );

        return response;
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
