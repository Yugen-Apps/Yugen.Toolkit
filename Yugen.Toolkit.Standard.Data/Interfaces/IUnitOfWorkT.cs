using System;
using Microsoft.EntityFrameworkCore;

namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}