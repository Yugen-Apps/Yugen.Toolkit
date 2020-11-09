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

        public EnumToBooleanConverterViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;

            ElementTheme = _themeSelectorService.Theme;
            SwitchThemeCommand = new AsyncRelayCommand<ElementTheme>(SwitchThemeCommandBehavior);
        }

        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => SetProperty(ref _elementTheme, value);
        }

        public ICommand SwitchThemeCommand { get; }

        private async Task SwitchThemeCommandBehavior(ElementTheme param)
        {
            ElementTheme = param;
            await _themeSelectorService.SetThemeAsync(param, false);
        }
    }
}