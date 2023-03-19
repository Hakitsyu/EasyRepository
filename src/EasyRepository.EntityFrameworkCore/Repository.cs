using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.EntityFrameworkCore
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private volatile bool _disposed;

        public DbContext Context { get; private set; }
        private DbSet<TEntity> Set => Context.Set<TEntity>();

        public Repository(DbContext context)
            => Context = context;

        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, 
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            => Set
                .NullableInclude(includeExpression)
                .NullableWhere(predicate)
                .ToList();

        public IList<TEntity> FindAll<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderKeySelector = null,
            Abstractions.OrderBy orderBy = Abstractions.OrderBy.Asc)
            => Set
                .NullableInclude(includeExpression)
                .NullableWhere(predicate)
                .NullableOrderBy(orderKeySelector, orderBy)
                .ToList();

        public TEntity? FindFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            => Set
                .NullableInclude(includeExpression)
                .NullableFirtOrDefault(predicate);

        public TEntity? FindFirstOrDefault<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderKeySelector = null,
            Abstractions.OrderBy orderBy = Abstractions.OrderBy.Asc)
            => Set
                .NullableInclude(includeExpression)
                .NullableOrderBy(orderKeySelector, orderBy)
                .NullableFirtOrDefault(predicate);

        public TEntity FindFirst(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null)
            => Set
                .NullableInclude(includeExpression)
                .NullableFindFirst(predicate);

        public TEntity FindFirst<TKey>(Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? includeExpression = null,
            Expression<Func<TEntity, TKey>>? orderKeySelector = null,
            Abstractions.OrderBy orderBy = Abstractions.OrderBy.Asc)
            => Set
                .NullableInclude(includeExpression)
                .NullableOrderBy(orderKeySelector, orderBy)
                .NullableFindFirst(predicate);

        public int Count(Expression<Func<TEntity, bool>>? predicate = null)
            => Set.NullableCount(predicate);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
            => await Set.NullableCountAsync(predicate);

        public bool Contains(TEntity entity)
            => Set.Contains(entity);

        public async Task<bool> ContainsAsync(TEntity entity)
            => await Set.ContainsAsync(entity);

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
            => Count(predicate) > 0;

        public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate)
            => await CountAsync(predicate) > 0;

        public void Create(TEntity entity, bool commit = true)
        {
            Set.Add(entity);
            CommitChanges(commit);
        }

        public async Task CreateAsync(TEntity entity, bool commit = true)
        {
            await Set.AddAsync(entity);
            await CommitChangesAsync(commit);
        }

        public void CreateRange(IList<TEntity> entities, bool commit = true)
        {
            Set.AddRange(entities);
            CommitChanges(commit);
        }

        public async Task CreateRangeAsync(IList<TEntity> entities, bool commit = true)
        {
            await Set.AddRangeAsync(entities);
            await CommitChangesAsync(commit);
        }

        public void Update(TEntity entity, bool commit = true)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            CommitChanges(commit);
        }

        public async Task UpdateAsync(TEntity entity, bool commit = true)
        {
            AttachModified(entity);
            await CommitChangesAsync(commit);
        }

        public void UpdateRange(IList<TEntity> entities, bool commit = true)
        {
            AttachModified(entities);
            CommitChanges(commit);
        }

        public async Task UpdateRangeAsync(IList<TEntity> entities, bool commit = true)
        {
            AttachModified(entities);
            await CommitChangesAsync(commit);
        }

        public void Save(TEntity entity, bool commit = true)
        {
            if (Contains(entity))
            {
                Update(entity, commit);
                return;
            }

            Create(entity, commit);
        }

        public async Task SaveAsync(TEntity entity, bool commit = true)
        {
            if (await ContainsAsync(entity))
            {
                await UpdateAsync(entity, commit);
                return;
            }

            await CreateAsync(entity, commit);
        }

        public void SaveRange(IList<TEntity> entities, bool commit = true)
        {
            foreach (var entity in entities)
            {
                if (Contains(entity))
                {
                    Update(entity, false);
                    continue;
                }

                Create(entity, false);
            }

            CommitChanges(commit);
        }

        public async Task SaveRangeAsync(IList<TEntity> entities, bool commit = true)
        {
            foreach (var entity in entities) 
            {
                if (await ContainsAsync(entity))
                {
                    await UpdateAsync(entity, false);
                    continue;
                }

                await CreateAsync(entity, false);
            }

            await CommitChangesAsync(commit);
        }

        public void Remove(TEntity entity, bool commit = true)
        {
            Set.Remove(entity);
            CommitChanges(commit);
        }

        public void RemoveRange(IList<TEntity> entities, bool commit = true)
        {
            Set.RemoveRange(entities);
            CommitChanges(commit);
        }

        public void CommitChanges(bool commit = true)
        {
            if (commit)
                Context.SaveChanges();
        }

        public async Task CommitChangesAsync(bool commit = true)
        {
            if (commit)
                await Context.SaveChangesAsync();
        }

        private void AttachModified(TEntity entity)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        private void AttachModified(IList<TEntity> entities)
        {
            Set.AttachRange(entities);
            foreach (var entity in entities)
                Context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Context.Dispose();
            }
        }
    }
}
