using EasyRepository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.Extensions.Specifications
{
    public static class IRepositoryExtensions
    {
        public static IList<TEntity> FindAll<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            where TEntity : class
            => source.FindAll(predicate: specification?.Spec ?? null,
                    includeExpression: includeExpression);

        public static IList<TEntity> FindAll<TEntity, TKey>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null,
            OrderBy orderBy = OrderBy.Asc)
            where TEntity : class
            => source.FindAll(predicate: specification?.Spec ?? null,
                includeExpression: includeExpression,
                orderByPredicate: orderByPredicate,
                orderBy: orderBy);

        public static TEntity? FindFirstOrDefault<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            where TEntity : class
            => source.FindFirstOrDefault(predicate: specification?.Spec ?? null,
                includeExpression: includeExpression);

        public static TEntity? FindFirstOrDefault<TEntity, TKey>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null,
            OrderBy orderBy = OrderBy.Asc)
            where TEntity : class
            => source.FindFirstOrDefault(predicate: specification?.Spec ?? null,
                includeExpression: includeExpression,
                orderByPredicate: orderByPredicate,
                orderBy: orderBy);

        public static TEntity FindFirst<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            where TEntity : class
            => source.FindFirst(predicate: specification?.Spec ?? null,
                includeExpression: includeExpression);

        public static TEntity FindFirst<TEntity, TKey>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderByPredicate = null,
            OrderBy orderBy = OrderBy.Asc)
            where TEntity : class
            => source.FindFirst(predicate: specification?.Spec ?? null,
                includeExpression: includeExpression,
                orderByPredicate: orderByPredicate,
                orderBy: orderBy);

        public static int Count<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null)
            where TEntity : class
            => source.Count(specification?.Spec ?? null);

        public static async Task<int> CountAsync<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity>? specification = null)
            where TEntity : class
            => await source.CountAsync(specification?.Spec ?? null);

        public static bool Contains<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity> specification)
            where TEntity : class
            => source.Contains(specification.Spec);

        public static async Task<bool> ContainsAsync<TEntity>(this IRepository<TEntity> source,
            Specification<TEntity> specification)
            where TEntity : class
            => await source.ContainsAsync(specification.Spec);
    }
}
