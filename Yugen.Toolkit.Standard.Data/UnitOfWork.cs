using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Yugen.Toolkit.Standard.Data.Interfaces;

namespace Yugen.Toolkit.Standard.Data
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext Context { get; }
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new BaseRepository<TEntity>(Context);
            }

            return (IBaseRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges<TEntity>(bool updateModified = true) where TEntity : BaseEntity
        {
            Audit<TEntity>(updateModified);
            return Context.SaveChanges();
        }

        private void Audit<TEntity>(bool updateModified = true) where TEntity : BaseEntity
        {
            var entries = Context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            var index = GetRepository<TEntity>().LastIndex();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    index++;
                    ((BaseEntity)entry.Entity).Index = index;
                    ((BaseEntity)entry.Entity).Created = DateTimeOffset.Now;
                }

                if (updateModified)
                    ((BaseEntity)entry.Entity).LastUpdated = DateTimeOffset.Now;
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}