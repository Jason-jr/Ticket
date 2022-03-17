using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Mediatr.Ticket.Handler;

public class CreateHandler : IRequestHandler<Request.Create, int>
{
    private readonly IDao<Domain.Entities.Ticket> _ticket;

    public CreateHandler(IDao<Domain.Entities.Ticket> ticket) => _ticket = ticket;

    public async Task<int> Handle(Request.Create request, CancellationToken cancellationToken)
    {
        var entity = await _ticket.AddAsync(new Domain.Entities.Ticket
        {
            Type = request.Type,
            Priority = request.Priority,
            Severity = request.Severity,
            Summary = request.Summary,
            Description = request.Description
        });

        return entity.Id;
    }
}