using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class DeleteHandler : IRequestHandler<Request.Delete>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public DeleteHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<Unit> Handle(Request.Delete request, CancellationToken cancellationToken)
    {
        var entity = await _ticket.GetFirstAsync(t => t.Id == request.Id, t => t);

        entity.Delete = true;
        await _ticket.UpdateAsync(entity);

        return Unit.Value;
    }
}