using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Interfaces;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

[Authorize]
public class AgendaController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMicrosoftGraphProvider _microsoftGraphProvider;
    private readonly GraphServiceClient _graphClient;

    public AgendaController(IMapper mapper, GraphServiceClient graphClient, IMicrosoftGraphProvider microsoftGraphProvider)
    {
        _mapper = mapper;
        _graphClient = graphClient;
        _microsoftGraphProvider = microsoftGraphProvider;
    }



    /// <remarks>
    /// Get agenda events in month
    /// </remarks>
    /// <item code="200">Found hooks</item>
    /// <item code="400">Invalid item</item>
    /// <item code="401">Unauthorized</item>
    [HttpGet("{year}/{month}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<AgendaEvent>), 200)]
    //[ProducesResponseType(typeof(AppConnectorError), 400)]
    //[ProducesResponseType(typeof(AppConnectorError), 404)]
    public async Task<IActionResult> GetAll(int year = -1, int month = -1)
    {
        var objectId = User.GetObjectId();
        if(year < 0) year = DateTime.Now.Year;
        if(month < 0) month = DateTime.Now.Month;


        var items = new List<AgendaEvent>();


        var res1 = await _graphClient.Users[objectId].Request().GetAsync();

        var calendars = await _graphClient.Users[objectId].Calendars.Request().GetAsync();

        var request = await _graphClient.Users[objectId].Events
            .Request()
            .WithAppOnly()
            .GetAsync();

        Console.WriteLine();

        //var items = await _microsoftGraphProvider.GetEventsAsync(objectid, year, month);

        return Ok(items);
    }

    ///// <remarks>
    ///// Update/Insert Connector hook
    ///// </remarks>
    ///// <item code="200">Found connector</item>
    ///// <item code="400">Invalid item</item>
    ///// <item code="401">Unauthorized</item>
    //[HttpPost]
    //[ProducesResponseType(typeof(IEnumerable<Agenda>), 200)]
    ////[ProducesResponseType(typeof(AppConnectorError), 400)]
    ////[ProducesResponseType(typeof(AppConnectorError), 404)]
    //public IActionResult Upsert([FromBody]Agenda payload)
    //{
    //    ItemHooks.Add(payload);
    //    return Ok();
    //}

    ///// <summary>
    ///// Required role: 'ATM Admin'
    ///// </summary>
    ///// <remarks>
    ///// Get the details of a registered Afas AppConnector.  
    ///// 
    ///// AfasEnv types:
    ///// - Production = 0 (default)
    ///// - Test       = 1
    ///// - Accept     = 2
    ///// 
    ///// </remarks>
    ///// <item code="200">Found connector</item>
    ///// <item code="400">Invalid item</item>
    ///// <item code="401">Unauthorized</item>
    ///// <item code="403">Forbidden, Missing role 'Atm.Admin'</item>
    ///// <item code="404">AppConnector not found</item>
    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(IEnumerable<Agenda>), 200)]
    ////[ProducesResponseType(typeof(AppConnectorError), 400)]
    ////[ProducesResponseType(typeof(AppConnectorError), 404)]
    //public IActionResult Delete(Guid id)
    //{
    //    return Ok();
    //}

}
