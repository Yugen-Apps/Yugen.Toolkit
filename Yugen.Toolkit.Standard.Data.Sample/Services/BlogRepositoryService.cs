using System.Collections.Generic;
using System.Linq;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;

namespace Yugen.Toolkit.Standard.Data.Sample.Services
{
    public class BlogRepositoryService : IBlogRepositoryService
    {
        private readonly IUnitOfWork _uow;

        public BlogRepositoryService(IUnitOfWork unit)
        {
            _uow = unit;
        }

        public void Add(Blog entity)
        {
            _uow.GetRepository<Blog>().Add(entity);
            _uow.SaveChanges<Blog>();
        }

        public List<Blog> Get() => _uow.GetRepository<Blog>().Get().ToList();
    }
}
