using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;

namespace Yugen.Toolkit.Standard.Data.Sample.Services
{
    public class Blog2Service : BaseService<Blog>, IBlog2Service
    {
        public Blog2Service(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
