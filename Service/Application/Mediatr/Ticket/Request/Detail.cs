using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Mediatr.Ticket.Request;

public class Detail : IRequest<TicketDetailVM>
{
    [FromRoute]
    public int Id { get; set; }
}