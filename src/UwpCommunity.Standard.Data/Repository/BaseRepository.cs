using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UwpCommunity.Standard.Data.Interfaces;
using UwpCommunity.Standard.Data.Models;

// https://github.com/threenine/Threenine.Data
// https://github.com/threenine/Threenine.Map
namespace UwpCommunity.Standard.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<T>();
        }


        public void Add(T entity) => DbSet.Add(entity);

        public void Add(IEnumerable<T> entities) => DbSet.AddRange(entities);


        public IQueryable<T> Get() => DbSet;

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);

        public IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.Where(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func) => func(DbSet).Where(predicate);


        public T Single(Guid id) => DbSet.Find(id);

        public T Single(Expression<Func<T, bool>> predicate) => DbSet.Single(predicate);

        public T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
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
            Func<IQueryable<T>, IQueryable<T>> func) => func(DbSet).Single(predicate);


        public T First(Expression<Func<T, DateTime>> predicate) => DbSet.OrderBy(predicate).First();

        public T Last(Expression<Func<T, DateTime>> predicate) => DbSet.OrderByDescending(predicate).First();


        public T First(Expression<Func<T, int>> predicate) => DbSet.OrderBy(predicate).First();

        public T Last(Expression<Func<T, int>> predicate) => DbSet.OrderByDescending(predicate).First();


        public void Update(T entity) => DbSet.Update(entity);

        public void Update(T entity, Guid id)
        {
            var originalEntity = DbSet.Find(id);
            DbContext.Entry(originalEntity).CurrentValues.SetValues(entity);
        }

        public void Update(params T[] entities) => DbSet.UpdateRange(entities);

        public void Update(IEnumerable<T> entities) => DbSet.UpdateRange(entities);


        public void Delete(T entity) => DbSet.Remove(entity);


        public Guid GetKey(T entity)
        {
            var keyName = DbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            var value = entity?.GetType().GetProperty(keyName)?.GetValue(entity, null)?.ToString();

            return value == null ? Guid.Empty : new Guid(value);
        }


        public int Count() => DbSet.Count();

        public bool IsEmpty() => !DbSet.Any();

        public int LastIndex() => DbSet.Any() ? DbSet.OrderByDescending(x => x.Index).First().Index : 0;
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

