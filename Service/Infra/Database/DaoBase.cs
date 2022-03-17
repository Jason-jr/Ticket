using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database;
public abstract class DaoBase<TEntity> : IDao<TEntity> where TEntity : class
{
    protected readonly IMapper _mapper;

    protected readonly TicketDbContext _context;

    private readonly DbSet<TEntity> _set;

    protected DaoBase(TicketDbContext context, IMapper mapper)
    {
        _set = context.Set<TEntity>();
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task AddEnumerableAsync(IEnumerable<TEntity> list)
    {
        await _set.AddRangeAsync(list);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> condition)
    {
        var remove = _set.Where(condition);
        _set.RemoveRange(remove);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> list)
    {
        _set.RemoveRange(list);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
    {
        return await _set.AnyAsync(condition);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
    {
        return await _set.AsNoTracking().Where(condition).CountAsync();
    }

    public virtual async Task<TResult> GetFirstAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selector = null, bool tracking = false)
    {
        var query = _set.Where(condition);
        query = tracking ? query : query.AsNoTracking();
        var result = selector == null ? query.ProjectTo<TResult>(_mapper.ConfigurationProvider) : query.Select(selector);
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<IList<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selector = null, bool tracking = false)
    {
        var query = _set.Where(condition);
        query = tracking ? query : query.AsNoTracking();
        var result = selector == null ? query.ProjectTo<TResult>(_mapper.ConfigurationProvider) : query.Select(selector);
        return await result.ToListAsync();
    }
}