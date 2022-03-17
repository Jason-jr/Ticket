using Application.Interfaces;
using Domain.Entities;

namespace Application.Models;

public class TicketAuthVM : IMap<RoleTicketType>
{
    public int Type { get; set; }

    public bool CanCreate { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public bool CanResolve { get; set; }
}