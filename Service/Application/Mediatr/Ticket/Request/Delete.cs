using MediatR;

namespace Application.Mediatr.Ticket.Request;

public class Delete : IRequest
{
    public int Id { get; set; }
}