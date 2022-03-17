using Application.Common;
using Application.Mediatr.User.Request;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize("admin")]
public class UserController : ApiControllerBase
{
    /// <summary>
    /// create user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> CreateAsync([FromBody] Create request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Get user list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<UserVM>), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] List request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Get user
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(UserDetailVM), 200)]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Detail key)
    {
        var response = await Mediator.Send(key);
        return Ok(response);
    }

    /// <summary>
    /// Update user
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
}