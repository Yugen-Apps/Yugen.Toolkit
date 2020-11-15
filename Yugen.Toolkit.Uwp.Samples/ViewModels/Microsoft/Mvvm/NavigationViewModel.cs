using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class NavigationViewModel : ViewModelBase
    {
        public void Button_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "I'm a parameter");
        }
    }
}