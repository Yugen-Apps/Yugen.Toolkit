using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    /// <summary>
    /// BaseRepository with CRUD operations
    /// (create, read, update, delete) 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Add an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Add the given entities with the given values to the database
        /// </summary>
        /// <param name="entities"></param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// Returns a sequence of all the entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition
        ///     <code>
        ///     Get(x => x.AttributeSetId.Equals(attributeSetId));
        ///     </code>
        /// </summary>
        /// <example>
        ///     <code>
        ///     Get(x => x.AttributeSetId.Equals(attributeSetId));
        ///     </code>
        /// </example>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a sequence of all the entities and
        /// specify related data to be included in query results
        ///     <code>
        ///     Get(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition and
        /// specify related data to be included in query results
        ///     <code>
        ///     Get(x => x.AttributeSetId.Equals(attributeSetId), 
        ///         x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns a sequence of entities that satisfies a specified condition and
        /// specify multiple level related data to be included in query results
        ///     <code>
        ///     Get(x => x.ProductId.Equals(productId), x => x
        ///      .Include(attributeSet => attributeSet.Image)
        ///      .Include(attributeSet => attributeSet.AttributeSetAttributeList)
        ///         .ThenInclude(attributeSetAttribute => attributeSetAttribute.Attribute)
        ///             .ThenInclude(attribute => attribute.AttributeType));
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeMultiLevelProperties"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties);

        /// <summary>
        /// Finds an entity with the given primary key values. 
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Single(Guid id);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition
        /// and throws an exception if more than one such element exists.
        ///     <code>
        ///     Single(x => x.AttributeSetId.Equals(attributeSetId));
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the only element of a sequence,
        /// specify related data to be included in query results
        /// and throws an exception if more than one such element exists.
        ///     <code>
        ///     Single(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T Single(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// specify related data to be included in query results
        /// and throws an exception if more than one such element exists.
        ///     <code>
        ///     Single(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// specify multiple level related data to be included in query results
        /// and throws an exception if more than one such element exists.
        ///     <code>
        ///     Single(x => x.ProductId.Equals(productId), x => x
        ///      .Include(attributeSet => attributeSet.Image)
        ///      .Include(attributeSet => attributeSet.AttributeSetAttributeList)
        ///         .ThenInclude(attributeSetAttribute => attributeSetAttribute.Attribute)
        ///             .ThenInclude(attribute => attribute.AttributeType));
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeMultiLevelProperties"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties);

        /// <summary>
        /// Returns the first element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T First(Expression<Func<T, DateTime>> predicate);

        /// <summary>
        /// Returns the last element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Last(Expression<Func<T, DateTime>> predicate);

        /// <summary>
        /// Returns the first element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T First(Expression<Func<T, int>> predicate);

        /// <summary>
        /// Returns the last element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Last(Expression<Func<T, int>> predicate);

        /// <summary>
        /// Update an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Update a detached entity with the given values and id to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        void Update(T entity, Guid id);

        /// <summary>
        /// Update the given entities with the given values to the database
        /// </summary>
        /// <param name="entities"></param>
        void Update(params T[] entities);

        /// <summary>
        /// Update the given entities with the given values to the database
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete the given entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Return the primary key of the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Guid GetKey(T entity);

        /// <summary>
        /// Returns the number of entities
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Return true if it contains at least one entity; otherwise, false.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Return the index of the last entity.
        /// </summary>
        /// <returns></returns>
        int LastIndex();
    }
}