using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class ValidationPage : Page
    {
        public ValidationPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<ValidationViewModel>();
        }

        private ValidationViewModel ViewModel => (ValidationViewModel)DataContext;

        private void Error_Click(object _1, RoutedEventArgs _2)
        {
            UsernameControl.CustomValidationMessage = "Username Already Exist";
            UsernameControl.IsCustomValid = false;
        }

        private async void Success_Click(object _1, RoutedEventArgs _2)
        {
            await ContentDialogHelper.Alert("Success", "", "Close");
        }
    }
}