using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm
{
    public sealed partial class CommandPage : Page
    {
        public CommandPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<CommandViewModel>();
        }

        private CommandViewModel ViewModel => (CommandViewModel)DataContext;
    }
}