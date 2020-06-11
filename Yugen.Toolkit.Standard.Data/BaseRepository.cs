using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yugen.Toolkit.Standard.Data.Interfaces;

// https://github.com/threenine/Threenine.Data
// https://github.com/threenine/Threenine.Map
namespace Yugen.Toolkit.Standard.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _bbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _bbContext = context;
            _dbSet = _bbContext.Set<T>();
        }


        public void Add(T entity) => _dbSet.Add(entity);

        public void Add(IEnumerable<T> entities) => _dbSet.AddRange(entities);


        public IQueryable<T> Get() => _dbSet;

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        public IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

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

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func) => func(_dbSet).Where(predicate);


        public T Single(Guid id) => _dbSet.Find(id);

        public T Single(Expression<Func<T, bool>> predicate) => _dbSet.Single(predicate);

        public T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Single(predicate);
        }

        /// <summary>
        /// Eager Load
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func) => func(_dbSet).Single(predicate);


        public T First(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderBy(predicate).First();

        public T Last(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderByDescending(predicate).First();


        public T First(Expression<Func<T, int>> predicate) => _dbSet.OrderBy(predicate).First();

        public T Last(Expression<Func<T, int>> predicate) => _dbSet.OrderByDescending(predicate).First();


        public void Update(T entity) => _dbSet.Update(entity);

        public void Update(T entity, Guid id)
        {
            var originalEntity = _dbSet.Find(id);
            _bbContext.Entry(originalEntity).CurrentValues.SetValues(entity);
        }

        public void Update(params T[] entities) => _dbSet.UpdateRange(entities);

        public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);


        public void Delete(T entity) => _dbSet.Remove(entity);


        public Guid GetKey(T entity)
        {
            var keyName = _bbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            var value = entity?.GetType().GetProperty(keyName)?.GetValue(entity, null)?.ToString();

            return value == null ? Guid.Empty : new Guid(value);
        }


        public int Count() => _dbSet.Count();

        public bool IsEmpty() => !_dbSet.Any();

        public int LastIndex() => _dbSet.Any() ? _dbSet.OrderByDescending(x => x.Index).First().Index : 0;
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

