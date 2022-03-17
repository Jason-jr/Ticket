using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Role.Handler;

public class ListHandler : IRequestHandler<Request.List, IList<RoleVM>>
{
    private readonly IDao<Domain.Entities.Role> _role;

    public ListHandler(IDao<Domain.Entities.Role> role) => _role = role;

    public async Task<IList<RoleVM>> Handle(Request.List request, CancellationToken cancellationToken)
    {
        return await _role.GetListAsync<RoleVM>(r => true);
    }
}