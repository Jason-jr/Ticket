using System.Security.Claims;
using System.Text.Json;
using Application.Common;
using Application.Mediatr.Auth.Request;
using Application.Mediatr.Role.Request;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class ConfigController : ApiControllerBase
{
    [HttpGet("tickettype")]
    [ProducesResponseType(typeof(IList<KeyValueVM<int>>), 200)]
    public async Task<IActionResult> TicketTypeAsync()
    {
        var auth =  await Mediator.Send(new Ticket { RoleIds = JsonSerializer.Deserialize<int[]>(User.FindFirstValue(ClaimTypes.Role)) });
        var types = Enum.GetValues<TicketType>().Select(pfm => pfm.Parse<int>()).ToList();
        var response = types.Where(t => auth.Any(a => t.Id == a.Type && (a.CanCreate || a.CanUpdate))).ToList();
        return Ok(response);
    }

    [HttpGet("ticketseverity")]
    [ProducesResponseType(typeof(IList<KeyValueVM<int>>), 200)]
    public IActionResult TicketSeverity()
    {
        var response = Enum.GetValues<TicketSeverity>().Select(pfm => pfm.Parse<int>()).ToList();
        return Ok(response);
    }

    [HttpGet("ticketpriority")]
    [ProducesResponseType(typeof(IList<KeyValueVM<int>>), 200)]
    public IActionResult TicketPriority()
    {
        var response = Enum.GetValues<TicketPriority>().Select(pfm => pfm.Parse<int>()).ToList();
        return Ok(response);
    }

    [HttpGet("role")]
    [ProducesResponseType(typeof(IList<RoleVM>), 200)]
    public async Task<IActionResult> RoleAsync([FromQuery] List request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}