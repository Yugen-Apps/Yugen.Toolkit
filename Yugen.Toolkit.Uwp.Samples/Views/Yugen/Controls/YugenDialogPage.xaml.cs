using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls
{
    public sealed partial class YugenDialogPage : Page
    {
        public YugenDialogPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<YugenDialogViewModel>();
        }

        private YugenDialogViewModel ViewModel => (YugenDialogViewModel)DataContext;
    }
}