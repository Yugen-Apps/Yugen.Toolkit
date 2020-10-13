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

        private RadioOption<ElementTheme> _elementThemeOption;

        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;

            ElementThemeOption = ElementThemeList.FirstOrDefault(x => x.Element.Equals(_themeSelectorService.Theme));
        }

        // Enum.GetValues(typeof(ElementTheme)).Cast<ElementTheme>().ToList();
        public List<RadioOption<ElementTheme>> ElementThemeList { get; } = new List<RadioOption<ElementTheme>>
        {
            new RadioOption<ElementTheme>(ElementTheme.Default),
            new RadioOption<ElementTheme>(ElementTheme.Dark),
            new RadioOption<ElementTheme>(ElementTheme.Light)
        };

        public RadioOption<ElementTheme> ElementThemeOption
        {
            get => _elementThemeOption;
            set
            {
                if (value != null && SetProperty(ref _elementThemeOption, value))
                {
                    _themeSelectorService.SetThemeAsync(_elementThemeOption.Element, false);
                }
            }
        }
    }
}