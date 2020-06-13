using System;
using Microsoft.EntityFrameworkCore;

namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        int SaveChanges<TEntity>(bool updateModified = true) where TEntity : BaseEntity;
    }
}