using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Auth.Request;

public class Ticket : IRequest<IList<TicketAuthVM>>
{
    public IList<int> RoleIds { get; set; }
}