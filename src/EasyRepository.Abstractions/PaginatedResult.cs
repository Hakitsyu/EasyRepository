using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRepository.Abstractions
{
    public class PaginatedResult<TEntity>
        where TEntity : class
    {
        public int Count { get; private set; }
        public int Page { get; private set; }
        public IReadOnlyCollection<TEntity> Entities { get; private set; }

        public PaginatedResult(IReadOnlyCollection<TEntity> entities, int page, int count)
            => (Entities, Page, Count) = (entities, page, count);
    }
}
