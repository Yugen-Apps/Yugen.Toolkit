using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValidationPage : Page
    {
        public ValidationViewModel ViewModel { get; set; } = new ValidationViewModel();

        public ValidationPage()
        {
            this.InitializeComponent();
        }

        private void ValidatingButtonControl_ErrorOnTapped(object sender, TappedRoutedEventArgs e)
        {
            UsernameControl.CustomValidationMessage = "Username Already Exist";
            UsernameControl.IsCustomValid = false;
        }

        private void ValidatingButtonControl_SuccessOnTapped(object sender, TappedRoutedEventArgs e)
        {
            _ = new MessageDialog("Success").ShowAsync();
        }
    }
}
