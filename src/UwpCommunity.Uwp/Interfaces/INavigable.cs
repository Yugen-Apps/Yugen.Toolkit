using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace UwpCommunity.Uwp.Interfaces
{
    public interface INavigable
    {
        void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state);
        Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending);
        //void OnNavigatingFrom(NavigatingEventArgs args);
    }
}