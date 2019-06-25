// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using UwpCommunity.Uwp.Samples.Views.Navigation;
using UwpCommunity.Uwp.Services;
using UwpCommunity.Uwp.ViewModels;

namespace UwpCommunity.Uwp.Samples.ViewModels.Navigation
{
    public class NavigationViewModel : BaseViewModel
    {
        public string ButtonContent { get; set; } = "Click Me";

        public void Button_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "Bello");
        }
    }
}
