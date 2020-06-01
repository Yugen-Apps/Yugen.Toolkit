// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Yugen.Toolkit.Uwp.Mvvm.ComponentModel;
using Yugen.Toolkit.Uwp.Samples.Views.Navigation;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class NavigationViewModel : ViewModelBase
    {
        public string ButtonContent { get; set; } = "Click Me";

        public void Button_Tapped(object _1, Windows.UI.Xaml.Input.TappedRoutedEventArgs _2)
        {
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "Bello");
        }
    }
}
