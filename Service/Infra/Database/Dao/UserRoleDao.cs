using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infra.Database.Dao;

public class UserRoleDao : DaoBase<UserRole>, IDao<UserRole>
{
    public UserRoleDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }
}