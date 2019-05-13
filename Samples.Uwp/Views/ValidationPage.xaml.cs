using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Samples.Uwp.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Samples.Uwp.Views
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

        private void ValidatingButtonControl_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            UsernameControl.CustomValidationMessage = "Username Already Exist";
            UsernameControl.IsCustomValid = false;
        }
    }
}
