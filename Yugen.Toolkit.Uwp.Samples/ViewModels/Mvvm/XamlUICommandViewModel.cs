using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Input;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Standard.Mvvm.Input;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class XamlUICommandViewModel : ViewModelBase
    {
        public ObservableCollection<Person> List = new ObservableCollection<Person>(Data.ContactList);

        private ICommand _copyCommand;

        public ICommand CopyCommand => _copyCommand
            ?? (_copyCommand = new RelayCommand<object>(CopyCommandBehavior));

        private void CopyCommandBehavior(object o)
        {
        }

        public void DeleteExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
        }
    }
}
