using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yugen.Toolkit.Uwp.CodeChallenge.Model;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface IDummyApiService
    {
        Task<IEnumerable<ValueModel>> GetValueModelsAsync();
    }
}
