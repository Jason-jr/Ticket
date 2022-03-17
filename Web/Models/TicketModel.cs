namespace Web.Models;

public class TicketDetailViewModel
{
    public int? Id { get; set; }

    public int? Type { get; set; }

    public TicketAuthModel Auth { get; set; }

    public TicketModel Ticket { get; set; }
}

public class TicketModel
{
    public int Type { get; set; }

    public int Priority { get; set; }

    public int Severity { get; set; }

    public bool IsResolve { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string TypeDescription { get; set; }

    public string PriorityDescription { get; set; }

    public string SeverityDescription { get; set; }
}

public class TicketAuthModel
{
    public int Type { get; set; }

    public bool CanCreate { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public bool CanResolve { get; set; }
}