using System.Security.Claims;
using Application.Interfaces;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid), out int id) ? id : null;
}