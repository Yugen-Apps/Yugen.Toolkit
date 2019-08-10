// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using UwpCommunity.Uwp.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace UwpCommunity.Uwp.Samples.ViewModels.Navigation
{
    public class NavigationParameterViewModel : BaseViewModel
    {
        //private string _parameter;

        private string _text;
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Text = parameter as string ?? string.Empty;
        }

        public void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Text = _parameter;
        }

        public void Button_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Text = "aaa";
        }
    }
}
