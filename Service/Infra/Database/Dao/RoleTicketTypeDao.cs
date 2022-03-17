using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infra.Database.Dao;

public class RoleTicketTypeDao : DaoBase<RoleTicketType>, IDao<RoleTicketType>
{
    public RoleTicketTypeDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }
}