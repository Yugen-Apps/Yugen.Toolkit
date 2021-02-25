using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class XamlUICommandViewModel : ViewModelBase
    {
        public ObservableCollection<Person> List = new ObservableCollection<Person>(DataConstants.ContactList);

        public XamlUICommandViewModel()
        {
            CopyCommand = new RelayCommand<object>(CopyCommandBehavior);
        }

        public IRelayCommand CopyCommand { get; }

        public void DeleteExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
        }

        private void CopyCommandBehavior(object o)
        {
        }
    }
}