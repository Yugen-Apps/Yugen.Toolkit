using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.Helpers;

namespace Yugen.Toolkit.Uwp.Samples.Views
{
    /// <summary>
    /// HomePage
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();

            var summary = DocHelper.ReadSummary("HomePage");
        }
    }
}