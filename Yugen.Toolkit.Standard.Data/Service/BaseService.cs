using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Data.Models;
using Yugen.Toolkit.Standard.Helpers;

// https://github.com/threenine/swcApi
namespace Yugen.Toolkit.Standard.Data.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public Result<T> Add(T entity, bool updateModified = true)
        {
            try
            {
                UnitOfWork.GetRepository<T>().Add(entity);
                UnitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        public Result<IEnumerable<T>> Get()
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Get().AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Get(predicate).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        public Result<IEnumerable<T>> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Get(includeProperties).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Get(predicate, includeProperties).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }

        public Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Get(predicate, func).AsEnumerable();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<IEnumerable<T>>($"{GetType()} {exception}");
            }
        }


        public Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>()
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

        public Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync,
            Func<IQueryable<T>, IQueryable<T>> func)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>()
                    .Get(x => x.LastUpdated >= lastSync, func)
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


        public Result<T> Single(Guid? id) => Single(id ?? Guid.Empty);

        public Result<T> Single(Guid id)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Single(id);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> Single(T entity)
        {
            try
            {
                var key = UnitOfWork.GetRepository<T>().GetKey(entity);
                var result = Single(key);
                return Result.IsOk(result.Value, result.Value != null, "");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> Single(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Single(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> Single(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Single(predicate, includeProperties);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Single(predicate, func);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        protected Result<T> First(Expression<Func<T, DateTime>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().First(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        protected Result<T> Last(Expression<Func<T, DateTime>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Last(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        protected Result<T> First(Expression<Func<T, int>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().First(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        protected Result<T> Last(Expression<Func<T, int>> predicate)
        {
            try
            {
                var entity = UnitOfWork.GetRepository<T>().Last(predicate);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        public Result<T> Update(T entity)
        {
            try
            {
                UnitOfWork.GetRepository<T>().Update(entity);
                UnitOfWork.SaveChanges<T>();
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> Update(T entity, Guid id, bool updateModified = true)
        {
            try
            {
                UnitOfWork.GetRepository<T>().Update(entity, id);
                UnitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(entity);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        public Result Delete(T entity)
        {
            try
            {
                UnitOfWork.GetRepository<T>().Delete(entity);
                UnitOfWork.SaveChanges<T>();
                return Result.Ok();
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail($"{GetType()} {exception}");
            }
        }

        public Result Delete(Guid id)
        {
            var entity = Single(id);
            return Delete(entity.Value);
        }

        public Result Delete(List<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                    UnitOfWork.GetRepository<T>().Delete(entity);

                UnitOfWork.SaveChanges<T>();
                return Result.Ok();
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail($"{GetType()} {exception}");
            }
        }


        public Result<T> SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            return Update(entity);
        }

        public Result<T> SoftDelete(Guid id)
        {
            var entity = Single(id);
            entity.Value.IsDeleted = true;
            return Update(entity.Value);
        }


        /// <summary>
        /// AddOrUpdateWithEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<T> AddOrUpdate(T entity) =>
            Single(entity).Failure ? Add(entity) : Update(entity);

        public Result<T> AddOrUpdate(List<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    if (Single(entity).Failure)
                        UnitOfWork.GetRepository<T>().Add(entity);
                    else
                        UnitOfWork.GetRepository<T>().Update(entity);
                }

                UnitOfWork.SaveChanges<T>();
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        /// <summary>
        /// AddOrUpdateWithoutEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateModified"></param>
        /// <returns></returns>
        public Result<T> AddOrUpdateWithoutEntity(T entity, bool updateModified = true)
        {
            if (Single(entity).Failure)
                return Add(entity, updateModified);

            var key = UnitOfWork.GetRepository<T>().GetKey(entity);
            return Update(entity, key, updateModified);
        }

        public Result<T> AddOrUpdateWithoutEntity(List<T> entityList, bool updateModified = true)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    if (Single(entity).Failure)
                    {
                        UnitOfWork.GetRepository<T>().Add(entity);
                    }
                    else
                    {
                        var key = UnitOfWork.GetRepository<T>().GetKey(entity);
                        UnitOfWork.GetRepository<T>().Update(entity, key);
                    }
                }

                UnitOfWork.SaveChanges<T>(updateModified);
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }


        public Result<int> Count()
        {
            try
            {
                var count = UnitOfWork.GetRepository<T>().Count();
                return Result.Ok(count);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<int>($"{GetType()} {exception}");
            }
        }

        public bool IsEmpty() => UnitOfWork.GetRepository<T>().IsEmpty();

        public int LastIndex() => UnitOfWork.GetRepository<T>().LastIndex();


        public Result<T> PushSync(List<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    var dbEntity = Single(entity);
                    if (dbEntity.Failure)
                    {
                        UnitOfWork.GetRepository<T>().Add(entity);
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
                            UnitOfWork.GetRepository<T>().Update(dbEntity.Value);
                        }
                        else
                        {
                            // Client is newer than server
                            entity.LastUpdated = DateTimeOffset.Now;
                            entity.ClientLastUpdated = entity.LastUpdated;
                            var key = UnitOfWork.GetRepository<T>().GetKey(entity);
                            UnitOfWork.GetRepository<T>().Update(entity, key);
                        }
                    }
                }

                UnitOfWork.SaveChanges<T>();
                return Result.Ok(default(T));
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(GetType(), exception);
                return Result.Fail<T>($"{GetType()} {exception}");
            }
        }

        public Result<T> PullSync(List<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    var dbEntity = Single(entity);
                    if (dbEntity.Failure)
                    {
                        UnitOfWork.GetRepository<T>().Add(entity);
                    }
                    else
                    {
                        entity.LastUpdated = DateTimeOffset.Now;
                        var key = UnitOfWork.GetRepository<T>().GetKey(entity);
                        UnitOfWork.GetRepository<T>().Update(entity, key);
                    }
                }

                UnitOfWork.SaveChanges<T>();
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