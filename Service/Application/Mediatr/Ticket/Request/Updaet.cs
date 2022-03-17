using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Enums;
using MediatR;

namespace Application.Mediatr.Ticket.Request;

public class Update : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public TicketPriority Priority { get; set; }

    [Required]
    public TicketSeverity Severity { get; set; }

    [Required]
    public string Summary { get; set; }

    [Required]
    public string Description { get; set; }
}