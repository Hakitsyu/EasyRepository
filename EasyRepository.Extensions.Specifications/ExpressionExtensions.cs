using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.Extensions.Specifications
{
    internal static class ExpressionExtensions
    {
        internal static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> source, Expression<Func<T, bool>> other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var binaryExpression = Expression.AndAlso(source.Body, other.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, source.Parameters.Single());
            return lambda;
        }

        internal static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> source, Expression<Func<T, bool>> other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var binaryExpression = Expression.OrElse(source.Body, other.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, source.Parameters.Single());
            return lambda;
        }
    }
}
