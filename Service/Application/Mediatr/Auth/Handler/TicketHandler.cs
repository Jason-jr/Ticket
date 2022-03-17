using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.Auth.Handler;

public class TicketHandler : IRequestHandler<Request.Ticket, IList<TicketAuthVM>>
{
    private readonly IDao<RoleTicketType> _roleTicket;

    public TicketHandler(IDao<RoleTicketType> roleTicket) => _roleTicket = roleTicket;

    public async Task<IList<TicketAuthVM>> Handle(Request.Ticket request, CancellationToken cancellationToken)
    {
        return await _roleTicket.GetListAsync<TicketAuthVM>(rt => request.RoleIds.Contains(rt.RoleId));
    }
}