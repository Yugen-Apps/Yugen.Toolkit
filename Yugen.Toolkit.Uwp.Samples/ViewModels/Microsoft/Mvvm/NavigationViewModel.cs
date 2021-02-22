using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            NavigateCommand = new RelayCommand(NavigateCommandBehavior);
        }

        public ICommand NavigateCommand { get; }

        public void NavigateCommandBehavior() => 
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "I'm a parameter");
    }
}