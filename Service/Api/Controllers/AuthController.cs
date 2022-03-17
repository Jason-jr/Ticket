using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Application.Common;
using Application.Mediatr.Auth.Request;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController : ApiControllerBase
{
    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] Login request)
    {
        var userId = await Mediator.Send(request);
        return userId == null ? Unauthorized() : Ok(JwtExtension.Token(userId.Value));
    }

    /// <summary>
    /// 角色
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("roles")]
    public async Task<IActionResult> UserRolesAsync()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid), out int userId))
            return Unauthorized();

        var roleIds = await Mediator.Send(new Roles { UserId = userId });
        return Ok(JwtExtension.Token(userId, roleIds.ToArray()));
    }

    /// <summary>
    /// 功能
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("functions")]
    [ProducesResponseType(typeof(IList<RoleFunctionVM>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RoleFunctionsAsync()
    {
        var response = await Mediator.Send(new Functions { RoleIds = JsonSerializer.Deserialize<int[]>(User.FindFirstValue(ClaimTypes.Role)) });
        return Ok(response);
    }

    /// <summary>
    /// Ticket權限
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("ticket")]
    [ProducesResponseType(typeof(IList<TicketAuthVM>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RoleTickeAsync()
    {
        var response = await Mediator.Send(new Ticket { RoleIds = JsonSerializer.Deserialize<int[]>(User.FindFirstValue(ClaimTypes.Role)) });
        return Ok(response);
    }
}