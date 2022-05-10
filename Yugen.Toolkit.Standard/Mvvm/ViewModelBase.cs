using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yugen.Toolkit.Standard.Mvvm.Interfaces;

namespace Yugen.Toolkit.Standard.Mvvm
{
    /// <summary>
    /// A base class for viewmodels.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, INavigable
    {
        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="state"></param>
        public virtual void OnNavigatedTo(object parameter, IDictionary<string, object> state) { /* nothing by default */ }

        /// <summary>
        /// Invoked immediately before the Page is unloaded and is no longer the current source of a parent Frame.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="suspending"></param>
        /// <returns></returns>
        public virtual async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { await Task.Yield(); }

        //public virtual void OnNavigatingFrom(NavigatingEventArgs args) { /* nothing by default */ }
    }
}