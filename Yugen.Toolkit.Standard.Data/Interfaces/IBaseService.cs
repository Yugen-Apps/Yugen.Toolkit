using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Core.Models;

namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    /// <summary>
    /// BaseService with CRUD operations
    /// (create, read, update, delete) 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : BaseEntity
    {
        /// <summary>
        /// Add an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateModified"></param>
        Result<T> Add(T entity, bool updateModified = true);

        /// <summary>
        /// Returns a sequence of all the entities
        /// </summary>
        /// <returns></returns>
        Result<IEnumerable<T>> Get();


        /// <summary>
        /// Returns a sequence of all entities changed after the DateTimeOffset
        /// </summary>
        /// <returns></returns>
        Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync);

        /// <summary>
        /// Finds an entity with the given primary key values. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<T> Single(Guid? id);
        /// <summary>
        /// Finds an entity with the given primary key values. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<T> Single(Guid id);
        /// <summary>
        /// Finds an entity with the given index values. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Result<T> Single(int index);
        /// <summary>
        /// Finds an entity with the given enitity values. 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Result<T> Single(T entity);

        /// <summary>
        /// Update an entity with the given values to the database
        /// </summary>
        /// <param name="entity"></param>
        Result<T> Update(T entity);
        /// <summary>
        /// Update a detached entity with the given values and id to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="updateModified"></param>
        Result<T> UpdateDetachedEntity(T entity, Guid id, bool updateModified = true);

        /// <summary>
        /// Delete the given entity
        /// </summary>
        /// <param name="entity"></param>
        Result Delete(T entity);
        /// <summary>
        /// Delete the given entity
        /// </summary>
        /// <param name="id"></param>
        Result Delete(Guid id);
        /// <summary>
        /// Delete the given entities
        /// </summary>
        /// <param name="itemList"></param>
        Result Delete(IEnumerable<T> itemList);

        /// <summary>
        /// SoftDelete the given entity
        /// </summary>
        /// <param name="entity"></param>
        Result<T> SoftDelete(T entity);
        /// <summary>
        /// SoftDelete the given entity
        /// </summary>
        /// <param name="id"></param>
        Result<T> SoftDelete(Guid id);

        /// <summary>
        /// AddOrUpdate the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Result<T> AddOrUpdate(T entity);
        /// <summary>
        /// AddOrUpdate the given entities
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        Result<T> AddOrUpdate(IEnumerable<T> itemList);

        /// <summary>
        /// AddOrUpdate the given detached entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateModified"></param>
        /// <returns></returns>
        Result<T> AddOrUpdateDetachedEntity(T entity, bool updateModified = true);
        /// <summary>
        /// AddOrUpdate the given detached entities
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="updateModified"></param>
        /// <returns></returns>
        Result<T> AddOrUpdateDetachedEntity(IEnumerable<T> itemList, bool updateModified = true);

        /// <summary>
        /// Returns the number of entities
        /// </summary>
        /// <returns></returns>
        Result<int> Count();
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

        /// <summary>
        /// Push and sync the entities changes
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Result<T> PushSync(IEnumerable<T> entityList);
        /// <summary>
        /// Pull and sync the entities changes
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Result<T> PullSync(IEnumerable<T> entityList);
    }
}