using CommunityToolkit.Mvvm.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            NavigateCommand = new RelayCommand(NavigateCommandBehavior);
        }

        public IRelayCommand NavigateCommand { get; }

        public void NavigateCommandBehavior() =>
            NavigationService.NavigateToPage(typeof(NavigationParameterPage), "I'm a parameter");
    }
}