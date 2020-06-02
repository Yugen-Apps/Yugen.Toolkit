using System.Collections.Generic;
using System.Threading.Tasks;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel.Interfaces;

namespace Yugen.Toolkit.Standard.Mvvm.ComponentModel
{
    /// <summary>
    /// A base class for viewmodels.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, INavigable
    {
        public virtual void OnNavigatedTo(object parameter, IDictionary<string, object> state) { /* nothing by default */ }
        public virtual async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { await Task.Yield(); }
        // public virtual void OnNavigatingFrom(NavigatingEventArgs args) { /* nothing by default */ }
    }
}