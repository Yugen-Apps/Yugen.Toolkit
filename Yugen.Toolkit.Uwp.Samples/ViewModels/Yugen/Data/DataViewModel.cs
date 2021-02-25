using Microsoft.Extensions.Logging;
using System.Linq;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Data
{
    public class DataViewModel : ViewModelBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<DataViewModel> _logger;

        public DataViewModel(IBlogService blogService, ILogger<DataViewModel> logger)
        {
            _blogService = blogService;
            _logger = logger;

            _blogService.Add(new Blog { Url = "aaa" });
            _logger.LogDebug("added");

            var list = _blogService.Get();
            _logger.LogDebug($"list: {list.Value.Count()}");

            if (list.Value.Count() > 0)
            {
                _logger.LogDebug($"item: {list.Value.FirstOrDefault()?.Url}");
            }
        }
    }
}