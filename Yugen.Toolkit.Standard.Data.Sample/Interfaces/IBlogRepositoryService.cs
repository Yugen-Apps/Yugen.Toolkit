using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data.Sample.Models;

namespace Yugen.Toolkit.Standard.Data.Sample.Interfaces
{
    public interface IBlogRepositoryService
    {
        void Add(Blog entity);
        List<Blog> Get();
    }
}
