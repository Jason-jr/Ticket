using MediatR;

namespace Application.Mediatr.Ticket.Request;

public class Resolve : IRequest
{
    public int Id { get; set; }
}