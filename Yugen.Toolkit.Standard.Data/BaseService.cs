using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Helpers;
using Yugen.Toolkit.Standard.Models;

namespace Yugen.Toolkit.Standard.Data
{
    /// <inheritdoc/>
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        /// <summary>
        /// _unitOfWork
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// BaseService
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public Result<T> Add(T entity, bool updateModified = true)
        {
            try
            {
                _unitOfWork.GetRepository<T>().Add(entity);
                _unitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<IEnumerable<T>> Get()
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Get().AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

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
        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Get(predicate).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns a sequence of all the entities and
        /// specify related data to be included in query results
        ///     <code>
        ///     Get(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<T>> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Get(includeProperties).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

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
        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Get(predicate, includeProperties).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

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
        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Get(predicate, includeMultiLevelProperties).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>()
                    .Get(x => x.LastUpdated >= lastSync)
                    .IgnoreQueryFilters()
                    .AsEnumerable();

                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns a sequence of all entities changed after the DateTimeOffset
        /// </summary>
        /// <param name="lastSync"></param>
        /// <param name="includeMultiLevelProperties"></param>
        /// <returns></returns>
        public Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>()
                    .Get(x => x.LastUpdated >= lastSync, includeMultiLevelProperties)
                    .IgnoreQueryFilters()
                    .AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<T> Single(Guid? id) => Single(id ?? Guid.Empty);

        /// <inheritdoc/>
        public Result<T> Single(Guid id)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(id);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public Result<T> Single(int index)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(x => x.Index.Equals(index));
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public Result<T> Single(T entity)
        {
            try
            {
                var key = _unitOfWork.GetRepository<T>().GetKey(entity);
                var result = Single(key);
                return Result.IsOk(result.Value, result.Value != null, "");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition.
        ///     <code>
        ///     Single(x => x.AttributeSetId.Equals(attributeSetId));
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> Single(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the only element of a sequence,
        /// specify related data to be included in query results.
        ///     <code>
        ///     Single(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public Result<T> Single(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(includeProperties);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// specify related data to be included in query results.
        ///     <code>
        ///     Single(x => x.Image, x => x.AttributeType);
        ///     </code>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public Result<T> Single(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(predicate, includeProperties);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition,
        /// specify multiple level related data to be included in query results.
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
        public Result<T> Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> includeMultiLevelProperties)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Single(predicate, includeMultiLevelProperties);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <summary>
        /// Returns the first element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> First(Expression<Func<T, DateTime>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().First(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the last element of a sequence ordered by DateTime
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> Last(Expression<Func<T, DateTime>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Last(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <summary>
        /// Returns the first element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> First(Expression<Func<T, int>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().First(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// Returns the last element of a sequence ordered by int
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> Last(Expression<Func<T, int>> predicate)
        {
            try
            {
                var entity = _unitOfWork.GetRepository<T>().Last(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<T> Update(T entity)
        {
            try
            {
                _unitOfWork.GetRepository<T>().Update(entity);
                _unitOfWork.SaveChanges<T>();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public Result<T> UpdateDetachedEntity(T entity, Guid id, bool updateModified = true)
        {
            try
            {
                _unitOfWork.GetRepository<T>().UpdateDetachedEntity(entity, id);
                _unitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result Delete(T entity)
        {
            try
            {
                _unitOfWork.GetRepository<T>().Delete(entity);
                _unitOfWork.SaveChanges<T>();
                return Result.Ok();
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public Result Delete(Guid id)
        {
            var entity = Single(id);
            return Delete(entity.Value);
        }

        /// <inheritdoc/>
        public Result Delete(IEnumerable<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                    _unitOfWork.GetRepository<T>().Delete(entity);

                _unitOfWork.SaveChanges<T>();
                return Result.Ok();
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<T> SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            return Update(entity);
        }

        /// <inheritdoc/>
        public Result<T> SoftDelete(Guid id)
        {
            var entity = Single(id);
            entity.Value.IsDeleted = true;
            return Update(entity.Value);
        }


        /// <inheritdoc/>
        public Result<T> AddOrUpdate(T entity) =>
            Single(entity).Failure ? Add(entity) : Update(entity);

        /// <inheritdoc/>
        public Result<T> AddOrUpdate(IEnumerable<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    if (Single(entity).Failure)
                        _unitOfWork.GetRepository<T>().Add(entity);
                    else
                        _unitOfWork.GetRepository<T>().Update(entity);
                }

                _unitOfWork.SaveChanges<T>();
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// AddOrUpdate the given detached entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateModified"></param>
        /// <returns></returns>
        public Result<T> AddOrUpdateDetachedEntity(T entity, bool updateModified = true)
        {
            if (Single(entity).Failure)
                return Add(entity, updateModified);

            var key = _unitOfWork.GetRepository<T>().GetKey(entity);
            return UpdateDetachedEntity(entity, key, updateModified);
        }

        /// <summary>
        /// AddOrUpdate the given detached entities
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="updateModified"></param>
        /// <returns></returns>
        public Result<T> AddOrUpdateDetachedEntity(IEnumerable<T> entityList, bool updateModified = true)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    if (Single(entity).Failure)
                    {
                        _unitOfWork.GetRepository<T>().Add(entity);
                    }
                    else
                    {
                        var key = _unitOfWork.GetRepository<T>().GetKey(entity);
                        _unitOfWork.GetRepository<T>().UpdateDetachedEntity(entity, key);
                    }
                }

                _unitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        /// <inheritdoc/>
        public Result<int> Count()
        {
            try
            {
                var count = _unitOfWork.GetRepository<T>().Count();
                return Result.Ok(count);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<int>($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public bool IsEmpty() => _unitOfWork.GetRepository<T>().IsEmpty();

        /// <inheritdoc/>
        public int LastIndex() => _unitOfWork.GetRepository<T>().LastIndex();


        /// <inheritdoc/>
        public Result<T> PushSync(IEnumerable<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    var dbEntity = Single(entity);
                    if (dbEntity.Failure)
                    {
                        _unitOfWork.GetRepository<T>().Add(entity);
                    }
                    else
                    {
                        if (dbEntity.Value.LastUpdated > entity.ClientLastUpdated)
                        {
                            // Conflict  
                            // Here you can just do a server, or client wins scenario, on a whole row basis.  
                            // E.g take the servers word or the clients word
                            // e.g. Server - wins - Ignore changes and just update time. 
                            dbEntity.Value.LastUpdated = DateTimeOffset.Now;
                            dbEntity.Value.ClientLastUpdated = entity.LastUpdated;
                            _unitOfWork.GetRepository<T>().Update(dbEntity.Value);
                        }
                        else
                        {
                            // Client is newer than server
                            entity.LastUpdated = DateTimeOffset.Now;
                            entity.ClientLastUpdated = entity.LastUpdated;
                            var key = _unitOfWork.GetRepository<T>().GetKey(entity);
                            _unitOfWork.GetRepository<T>().UpdateDetachedEntity(entity, key);
                        }
                    }
                }

                _unitOfWork.SaveChanges<T>();
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <inheritdoc/>
        public Result<T> PullSync(IEnumerable<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    var dbEntity = Single(entity);
                    if (dbEntity.Failure)
                    {
                        _unitOfWork.GetRepository<T>().Add(entity);
                    }
                    else
                    {
                        entity.LastUpdated = DateTimeOffset.Now;
                        var key = _unitOfWork.GetRepository<T>().GetKey(entity);
                        _unitOfWork.GetRepository<T>().UpdateDetachedEntity(entity, key);
                    }
                }

                _unitOfWork.SaveChanges<T>();
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }
    }
}