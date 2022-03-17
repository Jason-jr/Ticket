using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.Auth.Handler;

public class RolesHandler : IRequestHandler<Request.Roles, IList<int>>
{
    private readonly IDao<UserRole> _userRole;

    public RolesHandler(IDao<UserRole> userRole) => _userRole = userRole;

    public async Task<IList<int>> Handle(Request.Roles request, CancellationToken cancellationToken)
    {
        return await _userRole.GetListAsync(ur => ur.UserId == request.UserId, ur => ur.RoleId);
    }
}