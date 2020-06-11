using Microsoft.EntityFrameworkCore;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;

namespace Yugen.Toolkit.Standard.Data.Sample.Repository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(DbContext context) : base(context)
        {

        }
    }
}
