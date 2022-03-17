using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Ticket : AuditableEntity
{
    public int Id { get; set; }

    public TicketType Type { get; set; }

    public TicketPriority Priority { get; set; }

    public TicketSeverity Severity { get; set; }

    public bool IsResolve { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public bool Delete { get; set; }
}