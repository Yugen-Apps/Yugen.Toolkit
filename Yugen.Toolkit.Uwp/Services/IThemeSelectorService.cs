using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Yugen.Toolkit.Uwp.Services
{
    public interface IThemeSelectorService
    {
        ElementTheme Theme { get; set; }

        Task InitializeAsync(bool setTransparentTitleBar);
        Task SetRequestedThemeAsync();
        Task SetThemeAsync(ElementTheme theme, bool setTransparentTitleBar);
    }
}