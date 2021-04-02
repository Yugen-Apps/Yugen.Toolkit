using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls
{
    public sealed partial class GraphPage : Page
    {
        public GraphPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<GraphViewModel>();
        }

        private GraphViewModel ViewModel => (GraphViewModel)DataContext;
    }
}