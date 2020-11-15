using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    public sealed partial class MediatorPage : Page
    {
        public MediatorPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<MediatorViewModel>();
        }

        private MediatorViewModel ViewModel => (MediatorViewModel)DataContext;
    }
}