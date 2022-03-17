using Application.Common;
using Application.Mediatr.Ticket.Request;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class TicketController : ApiControllerBase
{
    /// <summary>
    /// Create ticket
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="200">ticket id</response>
    /// <response code="400">invalid request</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> CreateAsync([FromBody] Create request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Get ticket list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<TicketVM>), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] List request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Get ticket detail by id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTicketByIdAsync([FromRoute] Detail request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Update ticket
    /// </summary>
    /// <param name="key"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Detail key, [FromBody] Update request)
    {
        request.Id = key.Id;
        await Mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Resolve ticket
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpPut("{Id}/resolve")]
    public async Task<IActionResult> ResolveAsync([FromRoute] Detail key)
    {
        await Mediator.Send(new Resolve { Id = key.Id });
        return Ok();
    }

    /// <summary>
    /// Delete ticket
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Detail key)
    {
        await Mediator.Send(new Delete { Id = key.Id });
        return Ok();
    }
}