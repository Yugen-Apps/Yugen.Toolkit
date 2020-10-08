using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IThemeSelectorService _themeSelectorService;

        private ElementThemeOption _elementThemeOption;

        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;

            ElementThemeOption = ElementThemeList.FirstOrDefault(x => x.ElementTheme.Equals(_themeSelectorService.Theme));
        }

        // Enum.GetValues(typeof(ElementTheme)).Cast<ElementTheme>().ToList();
        public List<ElementThemeOption> ElementThemeList { get; } = new List<ElementThemeOption>
        {
            new ElementThemeOption(ElementTheme.Default),
            new ElementThemeOption(ElementTheme.Dark),
            new ElementThemeOption(ElementTheme.Light)
        };

        public ElementThemeOption ElementThemeOption
        {
            get => _elementThemeOption;
            set
            {
                if (value != null && SetProperty(ref _elementThemeOption, value))
                {
                    _themeSelectorService.SetThemeAsync(_elementThemeOption.ElementTheme, false);
                }
            }
        }
    }
}