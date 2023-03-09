using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Controllers;

public class ConnectorController : BaseController
{
    private readonly IMapper _mapper;

    public ConnectorController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery]string? query = null)
    {
        var items = Items;
        if (!string.IsNullOrEmpty(query))
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
