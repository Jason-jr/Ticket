using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using LinqKit;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class ListHandler : IRequestHandler<Request.List, IList<TicketVM>>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public ListHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<IList<TicketVM>> Handle(Request.List request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Domain.Entities.Ticket>(t => !t.Delete);
        if (!string.IsNullOrEmpty(request.Summary)) predicate = predicate.And(t => t.Summary.Contains(request.Summary));

        return await _ticket.GetListAsync<TicketVM>(predicate);
    }
}