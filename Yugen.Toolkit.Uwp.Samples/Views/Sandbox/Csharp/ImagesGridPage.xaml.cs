using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Sandbox.Csharp;

namespace Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Csharp
{
    public sealed partial class ImagesGridPage : Page
    {
        public ImagesGridPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<ImagesGridViewModel>();
        }

        private ImagesGridViewModel ViewModel => (ImagesGridViewModel)DataContext;
    }
}
