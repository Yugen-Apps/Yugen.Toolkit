using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class NavigationViewModel : ViewModelBase
    {
        public string ButtonContent { get; set; } = "Navigate To Next Page";

        public void Button_Tapped(object _1, Windows.UI.Xaml.Input.TappedRoutedEventArgs _2)
        {
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "I'm a parameter");
        }
    }
}
