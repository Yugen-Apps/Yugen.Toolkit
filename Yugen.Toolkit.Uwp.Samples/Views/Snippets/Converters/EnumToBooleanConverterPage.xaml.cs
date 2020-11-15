using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Snippets.Converters;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.Converters
{
    public sealed partial class EnumToBooleanConverterPage : Page
    {
        public EnumToBooleanConverterPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<EnumToBooleanConverterViewModel>();
        }

        private EnumToBooleanConverterViewModel ViewModel => (EnumToBooleanConverterViewModel)DataContext;
    }
}