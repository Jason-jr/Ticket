using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using MediatR;

namespace Application.Mediatr.Ticket.Request;

public class Create : IRequest<int>
{
    [Required]
    public TicketType Type { get; set; }

    [Required]
    public TicketPriority Priority { get; set; }

    [Required]
    public TicketSeverity Severity { get; set; }

    [Required]
    public string Summary { get; set; }

    [Required]
    public string Description { get; set; }
}