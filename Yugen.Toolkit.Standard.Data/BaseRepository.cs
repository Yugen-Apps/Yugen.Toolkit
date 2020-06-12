using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yugen.Toolkit.Standard.Data.Interfaces;

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

        /// <summary>
        /// Add an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity) => _dbSet.Add(entity);

        /// <summary>
        /// Add the given entities with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Add(IEnumerable<T> entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Returns a sequence of all the entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Get() => _dbSet;

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        /// <summary>
        /// Returns a sequence of all the entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition
        /// Eager Load
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func) => func(_dbSet).Where(predicate);

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the database
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Single(Guid id) => _dbSet.Find(id);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// and throws an exception if more than one such element exists.
        /// EG: Single(x => x.AttributeSetId.Equals(attributeSetId) &&
        ///     x.WarehouseId.Equals(wareHouseId)).Value;
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> predicate) => _dbSet.Single(predicate);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// and throws an exception if more than one such element exists.
        /// EG: Get(x => x.Image, x => x.AttributeType);
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// and throws an exception if more than one such element exists.
        /// Eager Load
        /// EG: Single(x => x.ProductId.Equals(productId), x => x
        ///      .Include(attributeSet => attributeSet.Image)
        ///      .Include(attributeSet => attributeSet.AttributeSetAttributeList)
        ///         .ThenInclude(attributeSetAttribute => attributeSetAttribute.Attribute)
        ///             .ThenInclude(attribute => attribute.AttributeType)).Value.ToList();
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func) => func(_dbSet).Single(predicate);

        /// <summary>
        /// Returns the first element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T First(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderBy(predicate).First();

        /// <summary>
        /// Returns the last element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Last(Expression<Func<T, DateTime>> predicate) => _dbSet.OrderByDescending(predicate).First();

        /// <summary>
        /// Returns the first element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T First(Expression<Func<T, int>> predicate) => _dbSet.OrderBy(predicate).First();

        /// <summary>
        /// Returns the last element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Last(Expression<Func<T, int>> predicate) => _dbSet.OrderByDescending(predicate).First();

        /// <summary>
        /// Update an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Update an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity, Guid id)
        {
            var originalEntity = _dbSet.Find(id);
            _bbContext.Entry(originalEntity).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// Update the given entities with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(params T[] entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Update the given entities with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Delete the given entities
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Return the primary key of the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid GetKey(T entity)
        {
            var keyName = _bbContext.Model.FindEntityType(
                typeof(T)).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();

            var value = entity?.GetType().GetProperty(keyName)
                ?.GetValue(entity, null)?.ToString();

            return value == null ? Guid.Empty : new Guid(value);
        }

        /// <summary>
        /// Returns the number of entities
        /// </summary>
        /// <returns></returns>
        public int Count() => _dbSet.Count();

        /// <summary>
        /// Return true if it contains at least one entity; otherwise, false.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => !_dbSet.Any();

        /// <summary>
        /// Return the index of the last entity.
        /// </summary>
        /// <returns></returns>
        public int LastIndex() => _dbSet.Any() 
            ? _dbSet.OrderByDescending(x => x.Index).First().Index : 0;
    }
}

// https://github.com/threenine/Threenine.Data

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

