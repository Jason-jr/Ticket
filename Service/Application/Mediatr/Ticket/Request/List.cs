using System.Collections.Generic;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Mediatr.Ticket.Request;

public class List : IRequest<IList<TicketVM>>
{
    [FromQuery]
    public string Summary { get; set; }
}