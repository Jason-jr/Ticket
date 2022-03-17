using Domain.Enums;

namespace Domain.Entities;

public class RoleTicketType
{
    public int RoleId { get; set; }

    public TicketType Type { get; set; }

    public bool CanCreate { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public bool CanResolve { get; set; }
}