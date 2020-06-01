using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Yugen.Toolkit.Uwp.Mvvm.ComponentModel.Interfaces;

namespace Yugen.Toolkit.Uwp.Mvvm.ComponentModel
{
    /// <summary>
    /// A base class for viewmodels.
    /// </summary>
    public abstract class ViewModelBase : Standard.Mvvm.ComponentModel.ViewModelBase, INavigable
    {

        public virtual void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state) { /* nothing by default */ }
        public virtual async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { await Task.Yield(); }
        // public virtual void OnNavigatingFrom(NavigatingEventArgs args) { /* nothing by default */ }
    }
}
