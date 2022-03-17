using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class UpdateHandler : IRequestHandler<Request.Update>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public UpdateHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<Unit> Handle(Request.Update request, CancellationToken cancellationToken)
    {
        var entity = await _ticket.GetFirstAsync(t => t.Id == request.Id, t => t);

        entity.Priority = request.Priority;
        entity.Severity = request.Severity;
        entity.Summary = request.Summary;
        entity.Description = request.Description;
        await _ticket.UpdateAsync(entity);

        return Unit.Value;
    }
}