using System;
using System.Collections.Generic;
using Common.Data.Models;
using Common.Standard.Data;

namespace Common.Data.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Result<T> Add(T entity, bool updateModified = true);

        Result<IEnumerable<T>> Get();
        //Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        //Result<IEnumerable<T>> Get(params Expression<Func<T, object>>[] includeProperties);
        //Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
        //    params Expression<Func<T, object>>[] includeProperties);
        //Result<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate,
        //    Func<IQueryable<T>, IQueryable<T>> func);

        Result<IEnumerable<T>> GetLastSyncChanges(DateTimeOffset lastSync);
        //Result<IEnumerable<T>> GetLastSyncChanges(DateTime lastSync, Func<IQueryable<T>, IQueryable<T>> func);

        Result<T> Single(Guid? id);
        Result<T> Single(Guid id);
        Result<T> Single(T entity);
        //Result<T> Single(Expression<Func<T, bool>> predicate);
        //Result<T> Single(Expression<Func<T, bool>> predicate,
        //    params Expression<Func<T, object>>[] includeProperties);
        //Result<T> Single(Expression<Func<T, bool>> predicate,
        //    Func<IQueryable<T>, IQueryable<T>> func);

        //Result<T> First(Expression<Func<T, DateTime>> predicate);
        //Result<T> Last(Expression<Func<T, DateTime>> predicate);

        //Result<T> First(Expression<Func<T, int>> predicate);
        //Result<T> Last(Expression<Func<T, int>> predicate);

        Result<T> Update(T entity);
        //Result<T> Update(T entity, int id, bool updateModified = true);

        Result Delete(T entity);
        Result Delete(Guid id);
        Result Delete(List<T> itemList);

        Result<T> SoftDelete(T entity);
        Result<T> SoftDelete(Guid id);

        Result<T> AddOrUpdate(T entity);
        Result<T> AddOrUpdate(List<T> itemList);

        Result<T> AddOrUpdateWithoutEntity(T entity, bool updateModified = true);
        Result<T> AddOrUpdateWithoutEntity(List<T> itemList, bool updateModified = true);

        Result<int> Count();
        bool IsEmpty();
        int LastIndex();

        Result<T> PushSync(List<T> entityList);
        Result<T> PullSync(List<T> entityList);
    }
}