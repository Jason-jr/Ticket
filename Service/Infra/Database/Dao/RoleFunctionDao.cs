using Application.Interfaces;
using Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Dao;

public class RoleFunctionDao : DaoBase<RoleFunction>, IRoleFunctionDao
{
    public RoleFunctionDao(TicketDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<IList<RoleFunctionVM>> GetFunctionsAsync(int[] roleIds)
    {
        return await _context.Function.Join(_context.RoleFunction, f => f.Id, rf => rf.FunctionId, (f, rf) => new RoleFuncitonJoin { F = f, RF = rf })
            .Where(j => roleIds.Contains(j.RF.RoleId))
            .GroupBy(j => new Function { Id = j.F.Id, Name = j.F.Name })
            .ProjectTo<RoleFunctionVM>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}