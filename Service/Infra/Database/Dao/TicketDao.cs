using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infra.Database.Dao;

public class TicketDao : DaoBase<Ticket>, IDao<Ticket>
{
    public TicketDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }
}