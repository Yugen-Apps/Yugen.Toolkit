using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels;

namespace Yugen.Toolkit.Uwp.Samples
{
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private AppShellViewModel ViewModel { get; } = Ioc.Default.GetService<AppShellViewModel>();
    }
}