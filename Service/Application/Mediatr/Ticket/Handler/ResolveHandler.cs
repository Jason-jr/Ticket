using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class ResolveHandler : IRequestHandler<Request.Resolve>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public ResolveHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<Unit> Handle(Request.Resolve request, CancellationToken cancellationToken)
    {
        var entity = await _ticket.GetFirstAsync(t => t.Id == request.Id, t => t);

        entity.IsResolve = true;
        await _ticket.UpdateAsync(entity);

        return Unit.Value;
    }
}