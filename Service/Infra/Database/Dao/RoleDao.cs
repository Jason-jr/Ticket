using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infra.Database.Dao;

public class RoleDao : DaoBase<Role>, IDao<Role>
{
    public RoleDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }
}