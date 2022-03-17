using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDao<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    Task AddEnumerableAsync(IEnumerable<TEntity> list);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(Expression<Func<TEntity, bool>> condition);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> condition);

    Task<TResult> GetFirstAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selector = null, bool tracking = false);

    Task<IList<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selector = null, bool tracking = false);
}