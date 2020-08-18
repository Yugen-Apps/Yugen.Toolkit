using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    public sealed partial class CommandPage : Page
    {
        public CommandPage()
        {
            this.InitializeComponent();
        }

        private CommandViewModel ViewModel { get; } = Ioc.Default.GetService<CommandViewModel>();
    }
}
