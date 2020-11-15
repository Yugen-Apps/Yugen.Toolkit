using Microsoft.Extensions.Logging;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ObservableObjects;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Mvvm
{
    public class ObservableSettingsViewModel : ViewModelBase
    {
        private readonly ITestService _testService;
        private readonly ILogger<ObservableSettingsViewModel> _logger;

        public ObservableSettingsViewModel(ITestService testService, ILogger<ObservableSettingsViewModel> logger)
        {
            _testService = testService;
            _logger = logger;

            var isDebug = Settings.Default.IsDebug;
            var url = Settings.Default.Url;
            var version = Settings.Default.Version;
            var rendering = Settings.Default.Rendering;

            Settings.Default.IsDebug = !isDebug;
            Settings.Default.Url = "url";
            Settings.Default.Version = 1;
            Settings.Default.Rendering = new Rendering
            {
                Url = "url",
                Version = 2
            };

            _testService.Test();

            var t = new TestService();
            t.Test();
        }
    }
}