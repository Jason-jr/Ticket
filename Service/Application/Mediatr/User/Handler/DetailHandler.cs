using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.User.Handler;

public class DetailHandler : IRequestHandler<Request.Detail, UserDetailVM>
{
    private readonly IDao<Domain.Entities.User> _user;

    private readonly IDao<UserRole> _userRole;

    public DetailHandler(IDao<Domain.Entities.User> user, IDao<UserRole> userRole)
    {
        _user = user;
        _userRole = userRole;
    }

    public async Task<UserDetailVM> Handle(Request.Detail request, CancellationToken cancellationToken)
    {
        var user = await _user.GetFirstAsync<UserDetailVM>(u => u.Id == request.Id);
        user.RoleIds = await _userRole.GetListAsync(ur => ur.UserId == user.Id, ur => ur.RoleId);
        return user;
    }
}