using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.Abstractions
{
    public interface IRepository<TEntity> : IDisposable
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

        int Count(Expression<Func<TEntity, bool>>? predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

        bool Contains(TEntity entity);
        Task<bool> ContainsAsync(TEntity entity);
        bool Contains(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);

        void Create(TEntity entity, bool commit = true);
        Task CreateAsync(TEntity entity, bool commit = true);
        void CreateRange(IList<TEntity> entities, bool commit = true);
        Task CreateRangeAsync(IList<TEntity> entities, bool commit = true);

        void Update(TEntity entity, bool commit = true);
        Task UpdateAsync(TEntity entity, bool commit = true);
        void UpdateRange(IList<TEntity> entities, bool commit = true);
        Task UpdateRangeAsync(IList<TEntity> entities, bool commit = true);

        void Save(TEntity entity, bool commit = true);
        Task SaveAsync(TEntity entity, bool commit = true);
        void SaveRange(IList<TEntity> entities, bool commit = true);
        Task SaveRangeAsync(IList<TEntity> entities, bool commit = true);

        void Remove(TEntity entity, bool commit = true);
        void RemoveRange(IList<TEntity> entities, bool commit = true);
    }
}
