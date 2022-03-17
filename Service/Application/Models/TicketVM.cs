using System;
using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models;

public class TicketVM : IMap<Ticket>
{
    public int Id { get; set; }

    public TicketType Type { get; set; }

    public string Summary { get; set; }

    public TicketPriority Priority { get; set; }

    public bool IsResolve { get; set; }

    public string TypeDescription => Type.Parse<int>().Value;

    public string PriorityDescription => Priority.Parse<int>().Value;
}

public class TicketDetailVM : IMap<Ticket>
{
    public int Id { get; set; }

    public TicketType Type { get; set; }

    public TicketPriority Priority { get; set; }

    public TicketSeverity Severity { get; set; }

    public bool IsResolve { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string TypeDescription => Type.Parse<int>().Value;

    public string PriorityDescription => Priority.Parse<int>().Value;

    public string SeverityDescription => Severity.Parse<int>().Value;

}