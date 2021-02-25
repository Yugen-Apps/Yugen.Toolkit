using Microsoft.Extensions.Logging;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;

namespace Yugen.Toolkit.Standard.Data.Sample.Services
{
    public class BlogService : BaseService<Blog>, IBlogService
    {
        public BlogService(IUnitOfWork unitOfWork, ILogger<BlogService> logger) : base(unitOfWork, logger) { }
    }
}
