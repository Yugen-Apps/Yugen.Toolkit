using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);

        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func);

        T Single(Guid id);
        T Single(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func);

        T First(Expression<Func<T, DateTime>> predicate);
        T Last(Expression<Func<T, DateTime>> predicate);

        T First(Expression<Func<T, int>> predicate);
        T Last(Expression<Func<T, int>> predicate);

        void Update(T entity);
        void Update(T entity, Guid id);

        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        Guid GetKey(T entity);

        int Count();
        bool IsEmpty();
        int LastIndex();
    }
}