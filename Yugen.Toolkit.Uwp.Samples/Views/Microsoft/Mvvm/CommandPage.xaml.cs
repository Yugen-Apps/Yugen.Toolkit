using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    public sealed partial class CommandPage : Page
    {
        public CommandPage()
        {
            this.InitializeComponent();

            DataContext = AppContainer.Services.GetService<CommandViewModel>();
        }

        private CommandViewModel ViewModel => (CommandViewModel)DataContext;
    }
}
