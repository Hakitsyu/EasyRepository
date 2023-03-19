using EasyRepository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EasyRepository.EntityFrameworkCore
{
    internal static class IQueryableExtensions
    {
        internal static IQueryable<T> NullableWhere<T>(this IQueryable<T> queryable, 
            Expression<Func<T, bool>>? predicate)
            => predicate != null ? queryable.Where(predicate) : queryable;

        internal static IQueryable<T> NullableOrderBy<T, TKey>(this IQueryable<T> queryable,
            Expression<Func<T, TKey>>? keySelector, OrderBy orderBy = OrderBy.Asc)
            => keySelector != null ? (
                orderBy == OrderBy.Asc ? queryable.OrderBy(keySelector) : queryable.OrderByDescending(keySelector)
            ) : queryable;

        internal static T? NullableFirtOrDefault<T>(this IQueryable<T> queryable,
            Expression<Func<T, bool>>? predicate)
            => predicate != null ? queryable.FirstOrDefault(predicate) : queryable.FirstOrDefault();

        internal static T NullableFindFirst<T>(this IQueryable<T> queryable,
            Expression<Func<T, bool>>? predicate)
            => predicate != null ? queryable.First(predicate) : queryable.First();

        internal static IQueryable<T> Include<T>(this IQueryable<T> source, Expression<Func<IQueryable<T>, IQueryable<T>>> includeExpression)
            => includeExpression.Compile().Invoke(source);

        internal static IQueryable<T> NullableInclude<T>(this IQueryable<T> source, Expression<Func<IQueryable<T>, IQueryable<T>>>? includeExpression)
            => includeExpression != null ? source.Include(includeExpression) : source;

        internal static int NullableCount<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate)
            => predicate != null ? source.Count(predicate) : source.Count();

        internal static async Task<int> NullableCountAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate)
            => await (predicate != null ? source.CountAsync(predicate) : source.CountAsync());
    }
}
