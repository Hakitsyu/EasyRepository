using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyRepository.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IList<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null);
        IList<TEntity> FindAll<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null, 
            OrderBy orderBy = OrderBy.Asc);

        TEntity? FindFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null);
        TEntity? FindFirstOrDefault<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null, 
            OrderBy orderBy = OrderBy.Asc);

        TEntity FindFirst(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null);
        TEntity FindFirst<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null, 
            OrderBy orderBy = OrderBy.Asc);
    }
}
