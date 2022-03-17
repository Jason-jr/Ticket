using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infra.Database.Dao;

public class UserDao : DaoBase<User>, IDao<User>
{
    public UserDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }
}