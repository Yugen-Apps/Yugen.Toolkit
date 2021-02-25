using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Data;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Data
{
    public sealed partial class DataPage : Page
    {
        public DataPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<DataViewModel>();
        }

        private DataViewModel ViewModel => (DataViewModel)DataContext;
    }
}
