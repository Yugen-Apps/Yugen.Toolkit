using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yugen.Toolkit.Standard.Data.Interfaces;

namespace Yugen.Toolkit.Standard.Data
{
    /// <inheritdoc/>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// _dbContext
        /// </summary>
        protected readonly DbContext _dbContext;
        /// <summary>
        /// _dbSet
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// BaseRepository
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        /// <inheritdoc/>
        public void Add(T entity) => _dbSet.Add(entity);

        /// <inheritdoc/>
        public void Add(IEnumerable<T> entities) => _dbSet.AddRange(entities);

        /// <inheritdoc/>
        public IQueryable<T> Get() => _dbSet;

        /// <inheritdoc/>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        /// <inheritdoc/>
        public IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        /// <inheritdoc/>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Where(predicate);
        }

        /// <inheritdoc/>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties) => includeMultiLevelProperties(_dbSet).Where(predicate);

        /// <inheritdoc/>
        public T Single(Guid id) => _dbSet.Find(id);

        /// <inheritdoc/>
        public T Single(Expression<Func<T, bool>> predicate) => _dbSet.Single(predicate);

        /// <inheritdoc/>
        public T Single(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Single();
        }

        /// <inheritdoc/>
        public T Single(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Single(predicate);
        }

        /// <inheritdoc/>
        public T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties) => includeMultiLevelProperties(_dbSet).Single(predicate);

        /// <inheritdoc/>
        public T First(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderBy(predicate).First();

        /// <inheritdoc/>
        public T Last(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderByDescending(predicate).First();

        /// <inheritdoc/>
        public T First(Expression<Func<T, int>> predicate) => _dbSet.OrderBy(predicate).First();

        /// <inheritdoc/>
        public T Last(Expression<Func<T, int>> predicate) => _dbSet.OrderByDescending(predicate).First();

        /// <inheritdoc/>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <inheritdoc/>
        public void UpdateDetachedEntity(T entity, Guid id)
        {
            var originalEntity = _dbSet.Find(id);
            _dbContext.Entry(originalEntity).CurrentValues.SetValues(entity);
        }

        /// <inheritdoc/>
        public void Update(params T[] entities) => _dbSet.UpdateRange(entities);

        /// <inheritdoc/>
        public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

        /// <inheritdoc/>
        public void Delete(T entity) => _dbSet.Remove(entity);

        /// <inheritdoc/>
        public Guid GetKey(T entity)
        {
            var keyName = _dbContext.Model.FindEntityType(
                typeof(T)).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();

            var value = entity?.GetType().GetProperty(keyName)
                ?.GetValue(entity, null)?.ToString();

            return value == null ? Guid.Empty : new Guid(value);
        }

        /// <inheritdoc/>
        public int Count() => _dbSet.Count();

        /// <inheritdoc/>
        public bool IsEmpty() => !_dbSet.Any();

        /// <inheritdoc/>
        public int LastIndex() => _dbSet.Any() 
            ? _dbSet.OrderByDescending(x => x.Index).First().Index : 0;
    }
}

//public void InsertOrUpdate(T item)
//{
//    var entityType = _context.Model.FindEntityType(typeof(T));
//    var primaryKey = entityType.FindPrimaryKey();
//    var keyValues = new object[primaryKey.Properties.Count];

//    for (int i = 0; i < keyValues.Length; i++)
//        keyValues[i] = primaryKey.Properties[i].GetGetter().GetClrValue(item);

//    var obj = _entities.Find(keyValues);
//    item.UpdatedOn = DateTime.UtcNow;

//    if (obj == null)
//    {
//        item.CreatedOn = DateTime.UtcNow;
//        _entities.Add(item);
//    }
//    else
//    {
//        _context.Entry(obj).CurrentValues.SetValues(item);
//    }

//    _context.SaveChanges();
//}

//public void InsertOrUpdateList(IEnumerable<T> itemList)
//{
//    var entityType = _context.Model.FindEntityType(typeof(T));
//    var primaryKey = entityType.FindPrimaryKey();
//    var keyValues = new object[primaryKey.Properties.Count];

//    foreach (T item in itemList)
//    {
//        for (int i = 0; i < keyValues.Length; i++)
//            keyValues[i] = primaryKey.Properties[i].GetGetter().GetClrValue(item);

//        var obj = _entities.Find(keyValues);
//        item.UpdatedOn = DateTime.UtcNow;

//        if (obj == null)
//        {
//            item.CreatedOn = DateTime.UtcNow;
//            _entities.Add(item);
//        }
//        else
//        {
//            _context.Entry(obj).CurrentValues.SetValues(item);
//        }
//    }
//    _context.SaveChanges();
//}

