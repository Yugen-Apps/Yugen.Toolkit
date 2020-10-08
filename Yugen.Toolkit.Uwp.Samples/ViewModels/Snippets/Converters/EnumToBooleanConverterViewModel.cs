using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Snippets.Converters
{
    public class EnumToBooleanConverterViewModel : ViewModelBase
    {
        private readonly IThemeSelectorService _themeSelectorService;

        private ElementTheme _elementTheme;
        private ICommand _switchThemeCommand;

        public EnumToBooleanConverterViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;

            ElementTheme = _themeSelectorService.Theme;
        }

        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => SetProperty(ref _elementTheme, value);
        }

        public ICommand SwitchThemeCommand => _switchThemeCommand ?? (_switchThemeCommand = new AsyncRelayCommand<ElementTheme>(SwitchThemeCommandBehavior));

        private async Task SwitchThemeCommandBehavior(ElementTheme param)
        {
            ElementTheme = param;
            await _themeSelectorService.SetThemeAsync(param, false);
        }
    }
}