using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class DetailHandler : IRequestHandler<Request.Detail, TicketDetailVM>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public DetailHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<TicketDetailVM> Handle(Request.Detail request, CancellationToken cancellationToken)
    {
        return await _ticket.GetFirstAsync<TicketDetailVM>(t => t.Id == request.Id);
    }
}