using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.Extensions.Specifications
{
    public class Specification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Spec { get; private set; }

        private Specification(Expression<Func<TEntity, bool>> spec)
            => Spec = spec;

        public Specification<TEntity> And(Specification<TEntity> otherSpec)
        {
            if (otherSpec == null)
                throw new ArgumentNullException(nameof(otherSpec));

            return new Specification<TEntity>(Spec.And(otherSpec.Spec));
        }

        public Specification<TEntity> Or(Specification<TEntity> otherSpec)
        {
            if (otherSpec == null)
                throw new ArgumentNullException(nameof(otherSpec));

            return new Specification<TEntity>(Spec.Or(otherSpec.Spec));
        }

        public static Specification<TEntity> Of(Expression<Func<TEntity, bool>> spec)
            => new Specification<TEntity>(spec);
    }
}
