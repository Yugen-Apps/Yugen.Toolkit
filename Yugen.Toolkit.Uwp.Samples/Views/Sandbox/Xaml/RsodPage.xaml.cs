using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using UnhandledExceptionEventArgs = Windows.UI.Xaml.UnhandledExceptionEventArgs;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Xaml
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RsodPage : Page
    {
        public string ExceptionText { get; internal set; }

        public UnhandledExceptionEventArgs Exception { get; internal set; }

        public RsodPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is UnhandledExceptionEventArgs exception)
            {
                ExceptionText = exception.Message + "\n\n" + exception.Exception.StackTrace;
            }
        }
    }
}
