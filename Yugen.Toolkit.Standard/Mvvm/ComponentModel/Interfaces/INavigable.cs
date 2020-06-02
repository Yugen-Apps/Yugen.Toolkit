using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yugen.Toolkit.Standard.Mvvm.ComponentModel.Interfaces
{
    public interface INavigable
    {
        void OnNavigatedTo(object parameter, IDictionary<string, object> state);
        Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending);
        // void OnNavigatingFrom(NavigatingEventArgs args);
    }
}