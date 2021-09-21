using System.Collections.Generic;
using System.Threading.Tasks;
using Yugen.Toolkit.Uwp.CodeChallenge.Model;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface IDataService
    {
        bool HasValues { get; }
        IList<ValueModel> Values { get; }

        Task Load();
        Task Save(IList<ValueModel> values);
    }
}
